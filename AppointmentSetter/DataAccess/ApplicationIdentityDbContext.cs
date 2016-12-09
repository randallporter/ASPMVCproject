using AppointmentSetter.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AppointmentSetter.DataAccess
{

    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationIdentityDbContext()
            : base("AppointmentSetterDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationIdentityDbContext Create()
        {
            return new ApplicationIdentityDbContext();
        }
    }
}