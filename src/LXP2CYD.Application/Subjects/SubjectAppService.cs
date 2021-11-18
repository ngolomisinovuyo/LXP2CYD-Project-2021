using Abp.Application.Services;
using Abp.Domain.Repositories;
using LXP2CYD.LearnerModels.Subjects;
using LXP2CYD.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Bursaries
{
    public class SubjectAppService: AsyncCrudAppService<Subject, SubjectDto, int, PagedSubjectResultRequestDto, CreateSubjectDto, SubjectDto>, ISubjectAppService
    {
        public SubjectAppService(IRepository<Subject, int> repository): base(repository)
        {

        }
    }
}
