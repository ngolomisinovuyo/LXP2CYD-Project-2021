using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Programmes
{
    public class ProgrammeAppService : AsyncCrudAppService<Programme, ProgrammeDto, int, PagedProgrammeResultRequestDto, CreateProgrammeDto, ProgrammeDto>, IProgrammeAppService
    {
        private readonly IRepository<Programme, int> _programmeRepository;
        private readonly IRepository<ProgrammeReservation, int> _programmeReservationRepository;
        public ProgrammeAppService(IRepository<Programme, int> repository,
            IRepository<Programme, int> programmeRepository,
            IRepository<ProgrammeReservation, int> programmeReservationRepository
            ) : base(repository)
        {
            _programmeRepository = programmeRepository;
            _programmeReservationRepository = programmeReservationRepository;
        }
        public override async Task<ProgrammeDto> CreateAsync(CreateProgrammeDto input)
        {
            var programme = ObjectMapper.Map<Programme>(input);
            programme.TenantId = AbpSession.TenantId;

            var id = await _programmeRepository.InsertAndGetIdAsync(programme);
            if (input.CreateProgrammeReservationDtos != null)
            {
                foreach (var createProgrammeReservationDto in input.CreateProgrammeReservationDtos)
                {
                    var appointmentAttendee = new ProgrammeReservation
                    {
                        UserId = createProgrammeReservationDto.UserId,
                        ProgrammeId = id,

                    };
                    await _programmeReservationRepository.InsertAsync(appointmentAttendee);
                }

            }
            return await GetAsync(new EntityDto<int>(id));
        }
    }
}
