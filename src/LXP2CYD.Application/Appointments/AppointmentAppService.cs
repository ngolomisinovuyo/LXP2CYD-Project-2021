using Abp.Application.Services;
using Abp.Domain.Repositories;
using LXP2CYD.Appointments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Appointments
{
    public class AppointmentAppService : AsyncCrudAppService<Appointment,AppointmentDto, int, PagedAppointmentResultResultDto, CreateAppointmentDto, AppointmentDto>, IAppointmentAppService
    {
        public AppointmentAppService(IRepository<Appointment, int> repository): base(repository)
        {

        }
    }
}
