namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_20_10_2019_add_tbl_for_diagnosis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alergies",
                c => new
                    {
                        AlergyId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        AlergyName = c.String(),
                    })
                .PrimaryKey(t => t.AlergyId);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionId = c.Int(nullable: false, identity: true),
                        DiagnosisId = c.Int(nullable: false),
                        PrescriptionName = c.String(),
                    })
                .PrimaryKey(t => t.PrescriptionId);
            
            CreateTable(
                "dbo.Symptoms",
                c => new
                    {
                        SymptomId = c.Int(nullable: false, identity: true),
                        DiagnosisId = c.Int(nullable: false),
                        SymptomName = c.String(),
                    })
                .PrimaryKey(t => t.SymptomId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Symptoms");
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Alergies");
        }
    }
}
