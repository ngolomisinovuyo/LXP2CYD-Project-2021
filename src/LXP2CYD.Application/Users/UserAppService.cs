 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.UI;
using LXP2CYD.Authorization;
using LXP2CYD.Authorization.Accounts;
using LXP2CYD.Authorization.Roles;
using LXP2CYD.Authorization.Users;
using LXP2CYD.Roles.Dto;
using LXP2CYD.Settings.Provinces;
using LXP2CYD.Settings.Provinces.Dto;
using LXP2CYD.Settings.Regions;
using LXP2CYD.Settings.Regions.Dto;
using LXP2CYD.Users.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LXP2CYD.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IRepository<Region, int> _regionRepository;
        private readonly IRepository<Province, int> _provinceRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            IWebHostEnvironment environment,
            IRepository<Region, int> regionRepository,
            IRepository<Province, int> provinceRepository,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _environment = environment;
            _regionRepository = regionRepository;
            _provinceRepository = provinceRepository;
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);
            user.UserName = input.EmailAddress;
            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
                foreach (var role in input.RoleNames)
                {
                    var userRole = _roleManager.Roles.Single(r => r.Name == role);

                    if (userRole != null)
                    {
                        List<Permission> grantedPermissions = new List<Permission>();
                        List<string> permissionsToGrant = new List<string>
                        {
                            PermissionNames.Pages_Users
                        };
                        if (userRole.Name == StaticRoleNames.Host.Admin)
                        {

                            permissionsToGrant = PermissionFinder.GetAllPermissions(new LXP2CYDAuthorizationProvider())
                         .Where(p => !p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant)).Select(x => x.Name).ToList();
                            grantedPermissions = PermissionManager
                             .GetAllPermissions()
                             .Where(p => permissionsToGrant.Contains(p.Name))
                             .ToList();
                        }
                        else if (userRole.Name == StaticRoleNames.Host.Provincial_Liaison || userRole.Name == StaticRoleNames.Host.Regional_Coordinator)
                        {
                            if (userRole.Name != StaticRoleNames.Host.Learner && userRole.Name != StaticRoleNames.Host.Employee)
                            {
                                permissionsToGrant.Add(PermissionNames.Pages_Tenants);
                                permissionsToGrant.Add(PermissionNames.Pages_Staff);
                            }
                            if (userRole.Name != StaticRoleNames.Host.Learner)
                            {
                                permissionsToGrant.Add(PermissionNames.Pages_Appointments);
                            }
                            if (userRole.Name == StaticRoleNames.Host.Provincial_Liaison)
                            {
                                permissionsToGrant.Add(PermissionNames.Pages_Coordinators);
                            }

                            grantedPermissions = PermissionManager
                            .GetAllPermissions()
                            .Where(p => permissionsToGrant.Contains(p.Name))
                            .ToList();


                        }
                        await _roleManager.SetGrantedPermissionsAsync(userRole, grantedPermissions);
                    }
                }


            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }
            
            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }
            
            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }
        public async Task<IReadOnlyList<ProvinceDto>> GetProvinces()
        {
            var provinces = await _provinceRepository.GetAllListAsync();
            return ObjectMapper.Map<IReadOnlyList<ProvinceDto>>(provinces);
        }
        public async Task<IReadOnlyList<RegionDto>> GetRegions()
        {
            var regions = await _regionRepository.GetAllListAsync();
            return ObjectMapper.Map<IReadOnlyList<RegionDto>>(regions);
        }
        private async Task SendConfirmation(UserConfirmAccountDto input)
        {
            var userId = _abpSession.UserId;
            //var user = _userManager.FirstOrDefault(x => x.Id == userId);
            //var tenant = await _tenantManager.GetByIdAsync((int)_session.TenantId);
            //send an email here
            string body = string.Empty;

            //using streamreader for reading my html template   

            var path = Path.Combine(_environment.WebRootPath, "templates/email/success-email.html");

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            string resetPasswordlink = $"{input.BaseUrl}/account/reset-password?code=" + input.Code + "&id=" + input.ToUserId;
            //string fromName = user.Name;
            string link = $"{input.BaseUrl}/account/tenant";
            body = body.Replace("#Name", input.Name);
            body = body.Replace("#Link", link);
            body = body.Replace("#ResetPasswordLink", resetPasswordlink);
            //body = body.Replace("#FromName", fromName);
            //body = body.Replace("#DomainName", tenant.TenancyName);
            body = body.Replace("#UserName", input.UserName);
            body = body.Replace("#Password", input.Password);
            //var credentials = await _accountService.GetSettings();
            //if (credentials.Count >= 5)
            //{
            //    await Emailer.SmptSend(to: input.Email, subject: "Sales Hack New Account Registration!", body: body, isBodyHtml: true, credentials);
            //}
            //else
            //{
            //    Emailer.Send(to: input.Email, subject: "Sales Hack New Account Registration!", body: body, fromEmail: user.EmailAddress, isBodyHtml: true);
            //}

        }

    }
}

