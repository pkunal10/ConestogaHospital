namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_20_11_2019_modify_tbl_patient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "EmailId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "EmailId");
        }
    }
}
