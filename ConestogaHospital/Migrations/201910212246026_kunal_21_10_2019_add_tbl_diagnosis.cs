namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_21_10_2019_add_tbl_diagnosis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        DiagnosisId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        DiagnosisDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Diagnosis");
        }
    }
}
