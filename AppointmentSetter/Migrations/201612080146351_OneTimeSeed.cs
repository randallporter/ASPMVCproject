namespace AppointmentSetter.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OneTimeSeed : DbMigration
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
        }

        public override void Down()
        {
            Sql("delete AppointmentTypes where ID in (1,2,3,4,5,6,7)");
        }
    }
}
