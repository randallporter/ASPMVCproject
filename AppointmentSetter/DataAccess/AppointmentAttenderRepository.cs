using AppointmentSetter.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AppointmentSetter.DataAccess
{
    public class AppointmentAttenderRepository : IAppointmentAttenderRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentAttenderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<AppointmentAttender> All
        {
            get { return _context.AppointmentAttenders; }
        }
        public IQueryable<AppointmentAttender> AllIncluding(params Expression<Func<AppointmentAttender, object>>[] includeProperties)
        {
            IQueryable<AppointmentAttender> qry = _context.AppointmentAttenders;
            foreach (var includeProperty in includeProperties)
            {
                qry = qry.Include(includeProperty);
            }
            return qry;
        }
        public AppointmentAttender Find(int ID)
        {
            return _context.AppointmentAttenders.Find(ID);
        }

        public void InsertOrUpdate(AppointmentAttender entity)
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
            var entity = Find(id);
            _context.AppointmentAttenders.Remove(entity);
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

    public interface IAppointmentAttenderRepository : IEntityRepository<AppointmentAttender>
    {
        //Added methods specific to IAppointmentRepo can go here
    }
}