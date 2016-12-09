using AppointmentSetter.Models;

namespace AppointmentSetter.DataAccess
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser getApplicationUser(string id)
        {
            return _context.Users.Find(id);
        }

    }

    public interface IApplicationUserRepository
    {
        ApplicationUser getApplicationUser(string id);
    }
}