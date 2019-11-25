namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_13_10_2019_add_tbl_tokens : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        PatientNo = c.String(),
                        DOB = c.DateTime(nullable: false),
                        AddressId = c.Int(nullable: false),
                        MobileNo = c.String(),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        TokenId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        GeneratedTime = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TokenId);
            
            CreateTable(
                "dbo.TokenStatus",
                c => new
                    {
                        TokenStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.TokenStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TokenStatus");
            DropTable("dbo.Tokens");
            DropTable("dbo.Patients");
        }
    }
}
