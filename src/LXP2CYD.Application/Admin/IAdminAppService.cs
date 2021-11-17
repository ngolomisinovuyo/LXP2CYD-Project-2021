using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LXP2CYD.Leaners.Dtos;
using LXP2CYD.MultiTenancy.Dto;
using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Admin
{
    public interface IAdminAppService: IApplicationService
    {
        Task<long> GetUserId();
        Task<LearnerDto> GetLearner(long? id = null);
        Task<ProgrammeDto> GetProgramme(int id);
        Task<PagedResultDto<ProgrammeDto>> GetProgrammes(PagedProgrammeResultRequestDto input);
        Task<TenantDto> GetTenant(int? id = null);
        Task<ProgrammeReservationDto> GetProgrammeReservation(int? id = null);
        Task<ProgrammeReservationDto> GetProgrammeReservationByUserAndProgrammeId(int programmeId, long userId);
        Task<IReadOnlyList<ProgrammeReservationDto>> GetProgrammeReservationsByUserId(long userId);
        Task<ProgrammeReservationDto> UpdateProgrammeReservation(ProgrammeReservationDto input);
        Task<ProgrammeReservationDto> CreateProgrammeReservation(CreateProgrammeReservationDto input);
        Task DeleteProgrammeReservation(int id);
    }
}
