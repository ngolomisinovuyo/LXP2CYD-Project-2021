using LXP2CYD.Appointments;
using System;

namespace LXP2CYD.Web.Models.Appointments
{
    public class AppointmentPdf
    {
        public string WebRootPath { get; set; }
        public DateTime Time { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string CenterNotes { get; set; }
        public AppointmentStatus Status { get; set; }
        public AppointmentType AppointmentType { get; set; }
    }
}
