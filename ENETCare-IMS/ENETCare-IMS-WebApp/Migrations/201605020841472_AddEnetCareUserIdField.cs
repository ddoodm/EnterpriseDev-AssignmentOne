namespace ENETCare.IMS.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnetCareUserIdField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EnetCareUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EnetCareUserId");
        }
    }
}
