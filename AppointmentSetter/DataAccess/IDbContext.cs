using AppointmentSetter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSetter.DataAccess
{
    public interface IDbContext : IDisposable
    {
        DbSet<Appointment> Appointments { get; set; }
        DbSet<AppointmentType> AppointmentTypes { get; set; }
        DbSet<User> Users { get; set; }
        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}
