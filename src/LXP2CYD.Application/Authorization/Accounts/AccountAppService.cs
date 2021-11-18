using System.IO;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using LXP2CYD.Authorization.Accounts.Dto;
using LXP2CYD.Authorization.Users;
using LXP2CYD.Users.Dto;
using Microsoft.AspNetCore.Hosting;

namespace LXP2CYD.Authorization.Accounts
{
    public class AccountAppService : LXP2CYDAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager _userManager;
        public AccountAppService(
            UserRegistrationManager userRegistrationManager,
            IWebHostEnvironment environment,
            UserManager userManager)
        {
            _userRegistrationManager = userRegistrationManager;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.Password,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = "https://localhost:5000/Account/ConfirmAccountcode=" + code + "&id=" + user.Id;
            await SendEmail(link, user.FullName, input.EmailAddress);
            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
        private async Task SendEmail(string link, string name, string email)
        {
            var userId = AbpSession.UserId;
           
            //send an email here
            string body = string.Empty;

            //using streamreader for reading my html template   

            var path = Path.Combine(_environment.WebRootPath, "Templates/Email/confirm-account.html");

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            //string fromName = user.Name;
            body = body.Replace("#Name", name);
            body = body.Replace("#Link", link);
           
            Emailer.Send(to: email, subject: "New Center Registration!", body: body, isBodyHtml: true);


        }
    }
}
