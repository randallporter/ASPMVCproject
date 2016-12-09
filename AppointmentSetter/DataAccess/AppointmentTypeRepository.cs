using AppointmentSetter.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AppointmentSetter.DataAccess
{
    public class AppointmentTypeRepository : IAppointmentTypeRepository
    {
        private readonly AppointmentDBContext _context;

        public AppointmentTypeRepository(AppointmentDBContext context)
        {
            _context = context;
        }
        public IQueryable<AppointmentType> All
        {
            get { return _context.AppointmentTypes; }
        }
        public IQueryable<AppointmentType> AllIncluding(params Expression<Func<AppointmentType, object>>[] includeProperties)
        {
            IQueryable<AppointmentType> qry = _context.AppointmentTypes;
            foreach (var includeProperty in includeProperties)
            {
                qry = qry.Include(includeProperty);
            }
            return qry;
        }
        public AppointmentType Find(int ID)
        {
            return _context.AppointmentTypes.Find(ID);
        }

        public void InsertOrUpdate(AppointmentType entity)
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
            _context.AppointmentTypes.Remove(entity);
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

    public interface IAppointmentTypeRepository : IEntityRepository<AppointmentType>
    {
        //Added methods specific to IAppointmentRepo can go here
    }
}