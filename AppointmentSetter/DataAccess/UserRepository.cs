using AppointmentSetter.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AppointmentSetter.DataAccess
{
    public class UserRepository : IUserRepository
    {
        public IDbContext _context;

        public UserRepository(IDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> All
        {
            get { return _context.Users; }
        }
        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> qry = _context.Users;
            foreach (var includeProperty in includeProperties)
            {
                qry = qry.Include(includeProperty);
            }
            return qry;
        }
        public User Find(int ID)
        {
            return _context.Users.Find(ID);
        }

        public void InsertOrUpdate(User entity)
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
            _context.Users.Remove(ac);
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

    public interface IUserRepository : IEntityRepository<User>
    {
        //Added methods specific to IUserRepository can go here
    }
}