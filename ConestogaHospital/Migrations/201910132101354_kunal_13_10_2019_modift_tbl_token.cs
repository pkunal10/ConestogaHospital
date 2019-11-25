namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_13_10_2019_modift_tbl_token : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tokens", "TokenNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tokens", "TokenNo");
        }
    }
}
