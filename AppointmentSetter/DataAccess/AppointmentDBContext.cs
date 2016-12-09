using AppointmentSetter.Models;
using System.Data.Entity;

namespace AppointmentSetter.DataAccess
{
    public class AppointmentDBContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        public DbSet<User> Users { get; set; }

        public AppointmentDBContext() : base("AppointmentSetterDB")
        {

        }

    }
}