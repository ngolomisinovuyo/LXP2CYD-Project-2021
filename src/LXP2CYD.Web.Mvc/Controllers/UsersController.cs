using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.Users;
using LXP2CYD.Web.Models.Users;
using Abp.Domain.Repositories;
using LXP2CYD.LearnerModels.Schools;
using System.Linq;
using LXP2CYD.Schools.Dtos;
using Microsoft.EntityFrameworkCore;
using LXP2CYD.Users.Dto;
using LXP2CYD.Authorization.Users.Staffs;
using LXP2CYD.LearnerModels.Learners;
using AutoMapper;
using LXP2CYD.Leaners.Dtos;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users,PermissionNames.Pages_Staff, PermissionNames.Pages_Learners)]
    public class UsersController : LXP2CYDControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IRepository<School, int> _schoolRepository;
        private readonly IRepository<Staff, long> _staffRepository;
        private readonly IRepository<Learner, long> _learnerRepository;
        private readonly IMapper _mapper;
        private readonly IAbpSession _session;
        public UsersController(IUserAppService userAppService, IRepository<School, int> schoolRepository,
            IRepository<Staff, long> staffRepository,
            IRepository<Learner, long> learnerRepository,
            IMapper mapper, IAbpSession session)
        {
            _userAppService = userAppService;
            _schoolRepository = schoolRepository;
            _staffRepository = staffRepository;
            _learnerRepository = learnerRepository;
            _mapper = mapper;
            _session = session;
        }

        public async Task<ActionResult> Index()
        {
            var roles = (await _userAppService.GetRoles()).Items;
            var regions = await _userAppService.GetRegions();
            var provinces = await _userAppService.GetProvinces();
            var model = new UserListViewModel
            {
                Provinces = provinces,
                Regions = regions,
                Roles = roles
            };
            return View(model);
        }

        public async Task<ActionResult> EditModal(long userId)
        {
            var user = await _userAppService.GetAsync(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var regions = await _userAppService.GetRegions();
            var provinces = await _userAppService.GetProvinces();
            var model = new EditUserModalViewModel
            {
                User = user,
                Provinces = provinces,
                Regions = regions,
                Roles = roles
            };
            return PartialView("_EditModal", model);
        }
        public async Task<ActionResult> Profile(long id)
        {
            var user = await _userAppService.GetAsync(new EntityDto<long>(id));
            user.Learner = await _userAppService.GetLearnerByUserId(id);
            user.Staff = await _userAppService.GetStaffByUserId(id);
            var provinces = await _userAppService.GetProvinces();
            var schools = await _schoolRepository.GetAll().Select(x => new SchoolDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            var model = new EditUserModalViewModel
            {
                Schools = schools,
                User = user,
                Provinces = provinces
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(UserDto input)
        {
            var user = await _userAppService.GetAsync(new EntityDto<long>(input.Id));
            await _userAppService.UpdateAsync(input);
            return RedirectToAction(nameof(Profile), new { id=input.Id});
        }
        [HttpPost]
        [DisableValidation]
        public async Task<ActionResult> EditStaff(StaffDto input)
        {
            var staff = await _staffRepository.FirstOrDefaultAsync(x=>x.Id == input.Id);
            if(staff != null)
            {
               
                _mapper.Map(input, staff);
                staff.Id = input.Id;
                await _staffRepository.UpdateAsync(staff);

            } else
            {
                staff = new Staff
                {
                    Duties = input.Duties,
                    UserId = input.UserId,
                    Type = StaffType.MENTOR,
                    TenantId = input.TenantId
                };
                await _staffRepository.InsertAsync(staff);
            }
        
            return RedirectToAction(nameof(Profile), new { id = input.UserId });
        }
        [HttpPost]
        [DisableValidation]
        public async Task<ActionResult> EditLearner(LearnerDto input)
        {
            var learner = await _learnerRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if(learner != null)
            {
                _mapper.Map(input, learner);
                learner.Id = input.Id;
                await _learnerRepository.UpdateAsync(learner);
            } else
            {
                learner = _mapper.Map<Learner>(input);
                learner.TenantId = _session.GetTenantId();
                await _learnerRepository.InsertAsync(learner);
            }
            return RedirectToAction(nameof(Profile), new { id = input.UserId });
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}
