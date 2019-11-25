namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_28_09_modify_tbl_doctor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "SpecialityId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "SpecialityId", c => c.String());
        }
    }
}
