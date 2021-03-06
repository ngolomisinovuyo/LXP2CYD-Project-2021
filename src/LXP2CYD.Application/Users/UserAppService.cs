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
using Abp.Domain.Uow;
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
using LXP2CYD.Authorization.Users.Staffs;
using LXP2CYD.Leaners.Dtos;
using LXP2CYD.LearnerModels.Learners;
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
    [AbpAuthorize(PermissionNames.Pages_Users, PermissionNames.Pages_Staff, PermissionNames.Pages_Learners)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Staff, long> _staffRepository;
        private readonly IRepository<Learner, long> _learnerRepository;
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
            IRepository<Staff, long> staffRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            IWebHostEnvironment environment,
            IRepository<Region, int> regionRepository,
            IRepository<Province, int> provinceRepository,
            IRepository<Learner, long> learnerRepository,
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
            _staffRepository = staffRepository;
            _learnerRepository = learnerRepository;
            _userRepository = repository;
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
                        List<string> permissionsToGrant = new List<string>();

                        var roleFound = _roleManager.Roles.Include(x => x.Permissions).Single(r => r.Name == role);

                        if (roleFound != null)
                        {
                            permissionsToGrant = roleFound.Permissions.Select(x => x.Name).ToList();
                        }

                        grantedPermissions = PermissionManager
                        .GetAllPermissions()
                        .Where(p => permissionsToGrant.Contains(p.Name))
                        .ToList();
                        await _roleManager.SetGrantedPermissionsAsync(userRole, grantedPermissions);
                        if (role == StaticRoleNames.Tenants.Employee || role == StaticRoleNames.Host.Volunteer)
                        {
                            var staff = new Staff()
                            {
                                UserId = user.Id,
                                TenantId = user.TenantId.Value,
                                Duties = "Mentoring, Uploading Learning Marerial, View Student Reports"
                            };
                            await _staffRepository.InsertAsync(staff);
                        }
                    }
                }


            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();
            input.TenantId = AbpSession.TenantId;

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

        
        public async Task<StaffDto> GetStaffByUserId(long userId)
        {
            var staff = await _staffRepository.GetAll().Include(x=>x.StaffSubjects)
                .ThenInclude(x=>x.Subject)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return ObjectMapper.Map<StaffDto>(staff);
        }
        public async Task<LearnerDto> GetLearnerByUserId(long userId)
        {
            var learner = await _learnerRepository.GetAll().Include(x=>x.LearnerSubjects)
                .ThenInclude(x=>x.Subject)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return ObjectMapper.Map<LearnerDto>(learner);
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
        private async Task SendEmail(UserConfirmAccountDto input)
        {
            var userId = _abpSession.UserId;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
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
              Emailer.Send(to: input.Email, subject: "New Center Registration!", body: body, fromEmail: input.Email, isBodyHtml: true);
            //}

        }

    }
}

