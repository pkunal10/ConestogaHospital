namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal08_10_2019_add_tbl_appointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        PatientNo = c.String(),
                        DoctorId = c.Int(nullable: false),
                        AppointmentDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}
