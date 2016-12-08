namespace AppointmentSetter.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;

    public partial class AdminAccount : DbMigration
    {
        public override void Up()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser { Email = "admin@appointment.com", UserName = "admin@appointment.com" };
            manager.Create(user, "PoIuYtR1@");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("admin"));
            manager.AddToRole(user.Id, "admin");

            context.AppointmentAttenders.Add(new AppointmentAttender { Title = "Lead Polisher", appointmentAttender = user });
            context.SaveChanges();
        }
        
        public override void Down()
        {
        }
    }
}
