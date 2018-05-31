namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Wheels", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "CheckIn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Vehicles", "Numberofwheels");
            DropColumn("dbo.Vehicles", "CheckInDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "CheckInDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vehicles", "Numberofwheels", c => c.Int(nullable: false));
            DropColumn("dbo.Vehicles", "CheckIn");
            DropColumn("dbo.Vehicles", "Wheels");
        }
    }
}
