using AppointmentSetter.Models;
using System.Data.Entity;

namespace AppointmentSetter.DataAccess
{
    public class AppointmentDBContext : DbContext , IDbContext 
    {
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentType> AppointmentTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public AppointmentDBContext() : base("AppointmentSetterDB")
        {

        }

    }
}