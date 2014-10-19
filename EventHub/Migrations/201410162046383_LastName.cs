namespace EventHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 256));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastName");
        }
    }
}
