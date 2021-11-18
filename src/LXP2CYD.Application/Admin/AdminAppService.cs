using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper.Configuration;
using LXP2CYD.Authorization.Accounts;
using LXP2CYD.Authorization.Users;
using LXP2CYD.Bursaries;
using LXP2CYD.Email;
using LXP2CYD.Leaners.Dtos;
using LXP2CYD.LearnerModels.Learners;
using LXP2CYD.MultiTenancy;
using LXP2CYD.MultiTenancy.Dto;
using LXP2CYD.Programmes;
using LXP2CYD.Programmes.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Admin
{
    public class AdminAppService: LXP2CYDAppServiceBase, IAdminAppService
    {
        private readonly IAccountAppService _accRepository;
        private readonly UserManager _userManager;
        private readonly TenantManager _teanntManager;
        //private readonly IAddressRepository _addressRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        IEmailAppService _emailService;
        private readonly IRepository<Learner, long> _learnerRepository;
        private readonly IRepository<Programme, int> _programmeRepository;
        private readonly IRepository<ProgrammeReservation, int> _programmeReservationRepository;
        private readonly IProgrammeReservationAppService _programmeReservationAppService;
        private readonly IProgrammeAppService _programmeAppService;

        public AdminAppService(
            IEmailAppService emailService,
            IConfiguration config,
            IAccountAppService accRepository,
            IWebHostEnvironment webHostEnvironment,
            UserManager userManager,
            IRepository<Learner, long> learnerRepository,
            IRepository<Programme, int> programmeRepository,
            IRepository<ProgrammeReservation, int>programmeReservationRepository,
            IProgrammeReservationAppService programmeReservationAppService,
            IProgrammeAppService programmeAppService)
        {
            _emailService = emailService;
            _config = config;
            _userManager = userManager;
            _accRepository = accRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _learnerRepository = learnerRepository;
            _programmeRepository = programmeRepository;
            _programmeReservationRepository = programmeReservationRepository;
            _programmeReservationAppService = programmeReservationAppService;
            _programmeAppService = programmeAppService;

        }

        async Task<long> GetUserId()
        {
            return AbpSession.UserId.Value;
        }

        public async Task<LearnerDto> GetLearner(long? id = null)
        {
            if (id == null)
                id = AbpSession.UserId;
            var learner =  await _learnerRepository.GetAsync(id.Value);
           return ObjectMapper.Map<LearnerDto>(learner);
        }

        public async Task<ProgrammeDto> GetProgramme(int id)
        {
            var programme = await _programmeRepository.GetAsync(id);
            return ObjectMapper.Map<ProgrammeDto>(programme);
        }

        public Task<TenantDto> GetTenant(int? id = null)
        {
            if (id == null)
                id = AbpSession.TenantId;
            throw new NotImplementedException();
        }

        public async Task<ProgrammeReservationDto> GetProgrammeReservation(int? id = null)
        {
           
            var programmeReservation = await _programmeReservationRepository.GetAsync(id.Value);
            return ObjectMapper.Map<ProgrammeReservationDto>(programmeReservation);
        }

        public async Task DeleteProgrammeReservation(int id)
        {
            await _programmeReservationRepository.DeleteAsync(id);
        }

        Task<long> IAdminAppService.GetUserId()
        {
            throw new NotImplementedException();
        }

        public async Task<ProgrammeReservationDto> GetProgrammeReservationByUserAndProgrammeId(int programmeId, long userId)
        {
            var programmeReservation = await _programmeReservationRepository.FirstOrDefaultAsync(x=>x.ProgrammeId == programmeId && x.UserId == userId);
            return ObjectMapper.Map<ProgrammeReservationDto>(programmeReservation);
        }

        public async Task<ProgrammeReservationDto> UpdateProgrammeReservation(ProgrammeReservationDto input)
        {
            return await _programmeReservationAppService.UpdateAsync(input);
            
        }

        public async Task<ProgrammeReservationDto> CreateProgrammeReservation(CreateProgrammeReservationDto input)
        {
            return await _programmeReservationAppService.CreateAsync(input);
        }

        public async Task<PagedResultDto<ProgrammeDto>> GetProgrammes(PagedProgrammeResultRequestDto input)
        {
            return await _programmeAppService.GetAllAsync(input);
        }

        public async Task<IReadOnlyList<ProgrammeReservationDto>> GetProgrammeReservationsByUserId(long userId)
        {
            return await _programmeReservationRepository.GetAll().Where(x => x.UserId == userId)
                .Select(x => new ProgrammeReservationDto
                {
                    Id = x.Id, ProgrammeId = x.ProgrammeId, UserId = x.UserId, Enrolled = x.Enrolled
                })
                .ToListAsync();
        }
    }
}
