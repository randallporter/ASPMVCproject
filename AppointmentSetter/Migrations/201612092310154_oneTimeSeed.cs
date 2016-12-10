namespace AppointmentSetter.Migrations
{
    using DataAccess;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;

    public partial class oneTimeSeed : DbMigration
    {
        public override void Up()
        {
            Sql("insert into AppointmentTypes (ID, Description, AppointmentLength) values (1, 'Wheel Polish', '00:30')");
            Sql("insert into AppointmentTypes (ID, Description, AppointmentLength) values (2, 'Trailer Polish', '04:00')");
            Sql("insert into AppointmentTypes (ID, Description, AppointmentLength) values (3, 'Quote/Meeting', '01:00')");
            Sql("insert into AppointmentTypes (ID, Description, AppointmentLength) values (4, 'Stack Polish', '02:00')");
            Sql("insert into AppointmentTypes (ID, Description, AppointmentLength) values (5, 'Engine Parts Polish', '00:45')");
            Sql("insert into AppointmentTypes (ID, Description, AppointmentLength) values (6, 'Grill Polish', '01:00')");
            Sql("insert into AppointmentTypes (ID, Description, AppointmentLength) values (7, 'Other', '02:00')");


            ApplicationIdentityDbContext context = new ApplicationIdentityDbContext();
            AppointmentDBContext mycontext = new AppointmentDBContext();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser { Email = "admin@appointment.com", UserName = "admin@appointment.com" };
            manager.Create(user, "Temp@55w0rd");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("admin"));
            manager.AddToRole(user.Id, "admin");
            context.SaveChanges();

            mycontext.Users.Add(new Models.User { IsCustomer = false, AppUserID = user.Id, userName = "admin@appointment.com" });
            mycontext.SaveChanges();
        }

        public override void Down()
        {
        }
    }
}
