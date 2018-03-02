namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFollowingTableColumnNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Followings", name: "ArtistId", newName: "FolloweeId");
            RenameColumn(table: "dbo.Followings", name: "UserId", newName: "FollowerId");
            RenameIndex(table: "dbo.Followings", name: "IX_UserId", newName: "IX_FollowerId");
            RenameIndex(table: "dbo.Followings", name: "IX_ArtistId", newName: "IX_FolloweeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Followings", name: "IX_FolloweeId", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.Followings", name: "IX_FollowerId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Followings", name: "FollowerId", newName: "UserId");
            RenameColumn(table: "dbo.Followings", name: "FolloweeId", newName: "ArtistId");
        }
    }
}
