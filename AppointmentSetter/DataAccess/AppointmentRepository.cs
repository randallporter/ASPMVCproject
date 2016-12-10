using AppointmentSetter.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AppointmentSetter.DataAccess
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public AppointmentDBContext _context;

        public AppointmentRepository()
        {
            
        }

        public void setContext(AppointmentDBContext context)
        {
            _context = context;
        }

        public IQueryable<Appointment> All
        {
            get { return _context.Appointments; }
        }
        public IQueryable<Appointment> AllIncluding(params Expression<Func<Appointment, object>>[] includeProperties)
        {
            IQueryable<Appointment> qry = _context.Appointments;
            foreach (var includeProperty in includeProperties)
            {
                qry = qry.Include(includeProperty);
            }
            return qry;
        }
        public Appointment Find(int ID)
        {
            return _context.Appointments.Find(ID);
        }

        public void InsertOrUpdate(Appointment entity)
        {
            if (entity.ID == default(int))
            {
                //This will only mark main entity as added, not other FK entities attached. 
                _context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        { 
            var ac = Find(id);
            _context.Appointments.Remove(ac);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IAppointmentRepository : IEntityRepository<Appointment>
    {
        //Added methods specific to IAppointmentRepo can go here
    }
}