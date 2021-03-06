using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LXP2CYD.Leaners.Dtos;
using LXP2CYD.Roles.Dto;
using LXP2CYD.Settings.Provinces.Dto;
using LXP2CYD.Settings.Regions.Dto;
using LXP2CYD.Users.Dto;

namespace LXP2CYD.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
        Task<IReadOnlyList<RegionDto>> GetRegions();
        Task<IReadOnlyList<ProvinceDto>> GetProvinces();
        Task<StaffDto> GetStaffByUserId(long userId);
        Task<LearnerDto> GetLearnerByUserId(long userId);
    }
}
