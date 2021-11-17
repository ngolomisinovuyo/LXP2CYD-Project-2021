using Abp.Application.Services;
using LXP2CYD.Appointments.Dto;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Appointments
{
    public interface IAppointmentAppService: IAsyncCrudAppService<AppointmentDto,int, PagedAppointmentResultRequestDto, CreateAppointmentDto,AppointmentDto>
    {
        Task<IReadOnlyList<UserDto>> GetAttendees();
    }
}
