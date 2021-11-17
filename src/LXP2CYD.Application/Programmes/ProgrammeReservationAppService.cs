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
    public class ProgrammeReservationAppService : AsyncCrudAppService<ProgrammeReservation, ProgrammeReservationDto, int, PagedResultRequestDto, CreateProgrammeReservationDto, ProgrammeReservationDto>, IProgrammeReservationAppService
    {
        public ProgrammeReservationAppService(IRepository<ProgrammeReservation, int> repository) : base(repository)
        {

        }

    }
}
