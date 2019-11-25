namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_28_09_add_tbls_for_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Line1 = c.String(),
                        City = c.String(),
                        Province = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SpecialityId = c.String(),
                        RoomNo = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        SpecialityId = c.Int(nullable: false, identity: true),
                        SpecialityName = c.String(),
                    })
                .PrimaryKey(t => t.SpecialityId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FName = c.String(),
                        LName = c.String(),
                        RoleId = c.Int(nullable: false),
                        EmailId = c.String(),
                        Mobile = c.String(),
                        AddressId = c.Int(nullable: false),
                        Password = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Specialities");
            DropTable("dbo.Doctors");
            DropTable("dbo.Addresses");
        }
    }
}
