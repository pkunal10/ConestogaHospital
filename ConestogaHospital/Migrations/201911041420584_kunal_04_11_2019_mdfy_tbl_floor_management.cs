namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_04_11_2019_mdfy_tbl_floor_management : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientAdmitDetails", "DischargeDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientAdmitDetails", "DischargeDate", c => c.DateTime(nullable: false));
        }
    }
}
