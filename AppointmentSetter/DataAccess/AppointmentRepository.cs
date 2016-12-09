using AppointmentSetter.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AppointmentSetter.DataAccess
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext context;

        public AppointmentRepository()
        {
            context = new ApplicationDbContext();
        }
        public IQueryable<Appointment> All
        {
            get { return context.Appointments; }
        }
        public IQueryable<Appointment> AllIncluding(params Expression<Func<Appointment, object>>[] includeProperties)
        {
            IQueryable<Appointment> qry = context.Appointments;
            foreach (var includeProperty in includeProperties)
            {
                qry = qry.Include(includeProperty);
            }
            return qry;
        }
        public Appointment Find(int ID)
        {
            return context.Appointments.Find(ID);
        }

        public void InsertOrUpdate(Appointment entity)
        {
            if (entity.ID == default(int))
            {
                //This will only mark main entity as added, not other FK entities attached. 
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        { 
            var ac = Find(id);
            context.Appointments.Remove(ac);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface IAppointmentRepository : IEntityRepository<Appointment>
    {

    }
}