using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using LXP2CYD.Authorization;
using LXP2CYD.Authorization.Roles;
using LXP2CYD.Authorization.Users;
using LXP2CYD.Editions;
using LXP2CYD.MultiTenancy.Dto;
using LXP2CYD.Settings.Regions;
using LXP2CYD.Settings.Regions.Dto;
using LXP2CYD.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LXP2CYD.MultiTenancy
{
    [AbpAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : AsyncCrudAppService<Tenant, TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IRepository<Region, int> _regionRepository;

        public TenantAppService(
            IRepository<Tenant, int> repository,
            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Region, int> regionRepository,
            IAbpZeroDbMigrator abpZeroDbMigrator)
            : base(repository)
        {
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _regionRepository = regionRepository;
            _abpZeroDbMigrator = abpZeroDbMigrator;
        }

        public override async Task<TenantDto> CreateAsync(CreateTenantDto input)
        {
            CheckCreatePermission();

            // Create tenant
            var tenant = ObjectMapper.Map<Tenant>(input); ;

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            await _tenantManager.CreateAsync(tenant);
            await CurrentUnitOfWork.SaveChangesAsync(); // To get new tenant's id.

            // Create tenant database
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            // We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                // Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); // To get static role ids

                // Grant all permissions to admin role

                List<string> permissionsToGrant = new List<string>();
                using(CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
                {
                    var centerManagerRole = _roleManager.Roles.Include(x=>x.Permissions).Single(r => r.Name == StaticRoleNames.Tenants.Center_Manager);
                    
                    if(centerManagerRole != null)
                    {
                        permissionsToGrant = centerManagerRole.Permissions.Select(x => x.Name).ToList();
                    }

                }
                var role = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Host.Admin);

                if (role != null)
                {
                    var grantedPermissions = PermissionManager
                           .GetAllPermissions()
                           .Where(p => permissionsToGrant.Contains(p.Name))
                           .ToList();
                    //await _roleManager.GrantAllPermissionsAsync(adminRole);
                    await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
                    // Create admin user for the role
                    if (input.MangerId == null)
                    {
                        var adminUser = User.CreateTenantAdminUser(tenant.Id, input.ManagerEmailAddress,
                        input.ManagerName, input.ManagerSurname);
                        await _userManager.InitializeOptionsAsync(tenant.Id);
                        CheckErrors(await _userManager.CreateAsync(adminUser, User.DefaultPassword));
                        await CurrentUnitOfWork.SaveChangesAsync(); // To get admin user's id

                        // Assign admin user to role!
                        CheckErrors(await _userManager.AddToRoleAsync(adminUser, role.Name));
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
                        {
                            var user = await _userManager.GetUserByIdAsync(input.MangerId.Value);
                            user.TenantId = tenant.Id;
                            await _userManager.UpdateAsync(user);
                        }

                    }
                }

                


            }

            return MapToEntityDto(tenant);
        }

        protected override IQueryable<Tenant> CreateFilteredQuery(PagedTenantResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.TenancyName.Contains(input.Keyword) || x.Name.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        public async override Task<PagedResultDto<TenantDto>> GetAllAsync(PagedTenantResultRequestDto input)
        {
            var totalCount = await Repository.GetAll()
                 .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .WhereIf(!input.Keyword.IsNullOrEmpty(), x => x.TenancyName.Contains(input.Keyword) ||
                x.Name.Contains(input.Keyword)).CountAsync();
            var tenants = await Repository.GetAll()
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .WhereIf(input.IsActive.HasValue, x =>x.IsActive == input.IsActive)
                .WhereIf(!input.Keyword.IsNullOrEmpty(), x=>x.TenancyName.Contains(input.Keyword) ||
                x.Name.Contains(input.Keyword))
                .Select(x => new TenantDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    TenancyName = x.TenancyName,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    IsActive = x.IsActive
                }).ToListAsync();
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                foreach (var tenant in tenants)
                {
                    var aminUsers = (await _userManager.GetUsersInRoleAsync(StaticRoleNames.Host.Admin));
                    var manager = aminUsers.FirstOrDefault(x => x.TenantId == tenant.Id);
                    tenant.Manager = ObjectMapper.Map<UserDto>(manager);
                }
            }
           
            return new PagedResultDto<TenantDto>(totalCount, tenants);
        }

        protected override void MapToEntity(TenantDto updateInput, Tenant entity)
        {
            // Manually mapped since TenantDto contains non-editable properties too.
            entity.Name = updateInput.Name;
            entity.TenancyName = updateInput.TenancyName;
            entity.IsActive = updateInput.IsActive;
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var tenant = await _tenantManager.GetByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        public async Task<IReadOnlyList<RegionDto>> GetRegions()
        {
            var regions = await _regionRepository.GetAllListAsync();
            return ObjectMapper.Map<IReadOnlyList<RegionDto>>(regions);
        }

    }
}

