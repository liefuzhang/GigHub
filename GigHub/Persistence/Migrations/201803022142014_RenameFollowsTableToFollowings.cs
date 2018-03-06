namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFollowsTableToFollowings : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Followings", newName: "Followings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Followings", newName: "Followings");
        }
    }
}
