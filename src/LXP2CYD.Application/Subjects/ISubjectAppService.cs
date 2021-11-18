using Abp.Application.Services;
using LXP2CYD.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Bursaries
{
    public interface ISubjectAppService: IAsyncCrudAppService<SubjectDto, int, PagedSubjectResultRequestDto, CreateSubjectDto, SubjectDto>
    {
    }
}
