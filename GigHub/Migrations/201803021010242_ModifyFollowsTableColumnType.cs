namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyFollowsTableColumnType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Followings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "Artist_Id" });
            DropIndex("dbo.Followings", new[] { "User_Id" });
            DropPrimaryKey("dbo.Followings");
            DropColumn("dbo.Followings", "FolloweeId");
            DropColumn("dbo.Followings", "FollowerId");
            RenameColumn(table: "dbo.Followings", name: "Artist_Id", newName: "FolloweeId");
            RenameColumn(table: "dbo.Followings", name: "User_Id", newName: "FollowerId");
            AlterColumn("dbo.Followings", "FollowerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Followings", "FolloweeId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Followings", "FolloweeId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Followings", "FollowerId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Followings", new[] { "FollowerId", "FolloweeId" });
            CreateIndex("dbo.Followings", "FollowerId");
            CreateIndex("dbo.Followings", "FolloweeId");
            AddForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "FolloweeId" });
            DropIndex("dbo.Followings", new[] { "FollowerId" });
            DropPrimaryKey("dbo.Followings");
            AlterColumn("dbo.Followings", "FollowerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Followings", "FolloweeId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Followings", "FolloweeId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Followings", "FollowerId", c => c.Byte(nullable: false));
            RenameColumn(table: "dbo.Followings", name: "FollowerId", newName: "User_Id");
            RenameColumn(table: "dbo.Followings", name: "FolloweeId", newName: "Artist_Id");
            AddColumn("dbo.Followings", "FollowerId", c => c.Byte(nullable: false));
            AddColumn("dbo.Followings", "FolloweeId", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Followings", new[] { "FollowerId", "FolloweeId" });
            CreateIndex("dbo.Followings", "User_Id");
            CreateIndex("dbo.Followings", "Artist_Id");
            AddForeignKey("dbo.Followings", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
