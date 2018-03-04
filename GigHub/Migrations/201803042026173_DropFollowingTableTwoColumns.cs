namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropFollowingTableTwoColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Followings", "UserId");
            DropColumn("dbo.Followings", "ArtistId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Followings", "UserId", c => c.Byte(nullable: false));
            AddColumn("dbo.Followings", "ArtistId", c => c.Byte(nullable: false));
        }
    }
}
