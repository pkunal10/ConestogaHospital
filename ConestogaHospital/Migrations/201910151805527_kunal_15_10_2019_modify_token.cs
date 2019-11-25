namespace ConestogaHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kunal_15_10_2019_modify_token : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tokens", "IsAnnounced", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tokens", "IsAnnounced");
        }
    }
}
