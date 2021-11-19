using Abp.Domain.Repositories;
using AutoMapper;
using LXP2CYD.Controllers;
using LXP2CYD.LearnerModels.Subjects;
using LXP2CYD.Subjects.Dto;
using LXP2CYD.Web.Models.Subjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Controllers
{
    public class SubjectsController : LXP2CYDControllerBase
    {
        private readonly IRepository<Subject, int> _subjectRepository;
        private readonly IMapper _mapper;
        public SubjectsController(IRepository<Subject, int> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectRepository.GetAll().Select(x => new SubjectDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
            var model = new SubejctListViewModel
            {
                Subjects = subjects
            };
            return View(model);
        }
    }
}
