namespace AppointmentSetter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentAndAppointmentAttenderAndType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Notes = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        appointmentAttender_ID = c.Int(nullable: false),
                        AppointmentSetter_Id = c.String(nullable: false, maxLength: 128),
                        appointmentType_ID = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppointmentAttenders", t => t.appointmentAttender_ID, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.AppointmentSetter_Id, cascadeDelete: false)
                .ForeignKey("dbo.AppointmentTypes", t => t.appointmentType_ID, cascadeDelete: false)
                .Index(t => t.appointmentAttender_ID)
                .Index(t => t.AppointmentSetter_Id)
                .Index(t => t.appointmentType_ID);
            
            CreateTable(
                "dbo.AppointmentAttenders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        appointmentAttender_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.appointmentAttender_Id, cascadeDelete: false)
                .Index(t => t.appointmentAttender_Id);
            
            CreateTable(
                "dbo.AppointmentTypes",
                c => new
                    {
                        ID = c.Byte(nullable: false),
                        Description = c.String(maxLength: 255),
                        AppointmentLength = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "appointmentType_ID", "dbo.AppointmentTypes");
            DropForeignKey("dbo.Appointments", "AppointmentSetter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "appointmentAttender_ID", "dbo.AppointmentAttenders");
            DropForeignKey("dbo.AppointmentAttenders", "appointmentAttender_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AppointmentAttenders", new[] { "appointmentAttender_Id" });
            DropIndex("dbo.Appointments", new[] { "appointmentType_ID" });
            DropIndex("dbo.Appointments", new[] { "AppointmentSetter_Id" });
            DropIndex("dbo.Appointments", new[] { "appointmentAttender_ID" });
            DropTable("dbo.AppointmentTypes");
            DropTable("dbo.AppointmentAttenders");
            DropTable("dbo.Appointments");
        }
    }
}
