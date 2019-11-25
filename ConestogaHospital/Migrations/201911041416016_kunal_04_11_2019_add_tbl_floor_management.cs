namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_04_11_2019_add_tbl_floor_management : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientAdmitDetails",
                c => new
                    {
                        DetailsId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        AdmitDate = c.DateTime(nullable: false),
                        DischargeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DetailsId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(),
                        FloorNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
            DropTable("dbo.PatientAdmitDetails");
        }
    }
}
