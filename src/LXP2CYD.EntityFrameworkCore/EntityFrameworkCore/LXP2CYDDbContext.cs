using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LXP2CYD.Authorization.Roles;
using LXP2CYD.Authorization.Users;
using LXP2CYD.MultiTenancy;
using LXP2CYD.Authorization.Users.Staffs;
using LXP2CYD.LearnerModels.Learners;
using LXP2CYD.YearPlans;
using LXP2CYD.Appointments;
using LXP2CYD.Settings.Provinces;
using LXP2CYD.Settings.Cities;
using LXP2CYD.Settings.Regions;
using LXP2CYD.Enquiries;
using LXP2CYD.Programmes;
using LXP2CYD.LearnerModels.Bursaries;
using LXP2CYD.LearnerModels.Subjects;
using LXP2CYD.LearnerModels.Schools;
using LXP2CYD.LearnerModels.Resources;
using LXP2CYD.LearnerModels.Enrollments;

namespace LXP2CYD.EntityFrameworkCore
{
    public class LXP2CYDDbContext : AbpZeroDbContext<Tenant, Role, User, LXP2CYDDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public LXP2CYDDbContext(DbContextOptions<LXP2CYDDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Learner> Learners { get; set; }
        public DbSet<YearPlan> YearPlans { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppintmentAttendee> AppintmentAttendees { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<ProgrammeReservation> ProgrammeReservations { get; set; }
        public DbSet<Bursary> Bursaries { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<StaffSubject> StaffSubjects { get; set; }
        public DbSet<LearnerSubject> LearnerSubjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
     
    }
}
