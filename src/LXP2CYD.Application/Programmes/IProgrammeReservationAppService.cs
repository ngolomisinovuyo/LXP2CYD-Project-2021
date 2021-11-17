using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Programmes
{
    public interface IProgrammeReservationAppService: IAsyncCrudAppService<ProgrammeReservationDto, int, PagedResultRequestDto, CreateProgrammeReservationDto, ProgrammeReservationDto>
    {
    }
}
