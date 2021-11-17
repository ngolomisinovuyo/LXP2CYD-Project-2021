using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using LXP2CYD.Appointments.Dto;
using LXP2CYD.Authorization.Roles;
using LXP2CYD.Authorization.Users;
using LXP2CYD.Users.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Appointments
{
    public class AppointmentAppService : AsyncCrudAppService<Appointment,AppointmentDto, int, PagedAppointmentResultRequestDto, CreateAppointmentDto, AppointmentDto>, IAppointmentAppService
    {
        private readonly IRepository<Appointment, int> _appointmentRepository;
        private readonly IRepository<AppointmentAttendee, int> _appointmentAttendeeRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        public AppointmentAppService(IRepository<Appointment, int> repository,
            IRepository<User, long> userRepository,
            IRepository<AppointmentAttendee, int> appointmentAttendeeRepository,
            RoleManager roleManager,
            UserManager userManager) : base(repository)
        {
            _appointmentRepository = repository;
            _userRepository = userRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _appointmentAttendeeRepository = appointmentAttendeeRepository;
        }
        public override async Task<AppointmentDto> CreateAsync(CreateAppointmentDto input)
        {
            var appointment = ObjectMapper.Map<Appointment>(input);
            appointment.HostId = AbpSession.UserId.Value;
            appointment.TenantId = AbpSession.TenantId;
            
            var id = await _appointmentRepository.InsertAndGetIdAsync(appointment);
            if(input.CreateAppointmentAttendeeDtos != null)
            {
                foreach(var creteAppointmentAttendeeDto in input.CreateAppointmentAttendeeDtos)
                {
                    var appointmentAttendee = new AppointmentAttendee
                    {
                        AttendeeId = creteAppointmentAttendeeDto.AttendeeId,
                        EmailAddress = creteAppointmentAttendeeDto.EmailAddress,
                        AppointmentId = id,
                        Time = appointment.StartTime,
                        TenantId = AbpSession.TenantId,
                    };
                   
                    await _appointmentAttendeeRepository.InsertAsync(appointmentAttendee);
                }
                
            }
            return await GetAsync(new EntityDto<int>(id));
        }
        public async Task<IReadOnlyList<UserDto>> GetAttendees()
        {
            List<UserDto> users = new List<UserDto>();
            using(CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                users = await _userRepository.GetAll().Where(x => x.IsActive && x.Id != AbpSession.TenantId).Select(x => new UserDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    IsActive = x.IsActive
                }).ToListAsync();
            }
            
            
            return users;
        }
    }
}
