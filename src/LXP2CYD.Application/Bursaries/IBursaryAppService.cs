using Abp.Application.Services;
using LXP2CYD.Bursaries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Bursaries
{
    public interface IBursaryAppService: IAsyncCrudAppService<BursaryDto, int, PagedBursaryResultRequestDto, CreateBursaryDto, BursaryDto>
    {
    }
}
