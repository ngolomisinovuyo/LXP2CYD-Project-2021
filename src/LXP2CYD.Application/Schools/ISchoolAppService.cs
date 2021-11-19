using Abp.Application.Services;
using LXP2CYD.Schools.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Bursaries
{
    public interface ISchoolAppService: IAsyncCrudAppService<SchoolDto, int, PagedSchoolResultRequestDto, CreateSchoolDto, SchoolDto>
    {
    }
}
