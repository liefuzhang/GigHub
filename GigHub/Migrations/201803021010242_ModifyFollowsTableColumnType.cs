namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyFollowsTableColumnType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Follows", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "Artist_Id" });
            DropIndex("dbo.Follows", new[] { "User_Id" });
            DropPrimaryKey("dbo.Follows");
            DropColumn("dbo.Follows", "ArtistId");
            DropColumn("dbo.Follows", "UserId");
            RenameColumn(table: "dbo.Follows", name: "Artist_Id", newName: "ArtistId");
            RenameColumn(table: "dbo.Follows", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Follows", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Follows", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Follows", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Follows", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Follows", new[] { "UserId", "ArtistId" });
            CreateIndex("dbo.Follows", "UserId");
            CreateIndex("dbo.Follows", "ArtistId");
            AddForeignKey("dbo.Follows", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "ArtistId" });
            DropIndex("dbo.Follows", new[] { "UserId" });
            DropPrimaryKey("dbo.Follows");
            AlterColumn("dbo.Follows", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Follows", "ArtistId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Follows", "ArtistId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Follows", "UserId", c => c.Byte(nullable: false));
            RenameColumn(table: "dbo.Follows", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Follows", name: "ArtistId", newName: "Artist_Id");
            AddColumn("dbo.Follows", "UserId", c => c.Byte(nullable: false));
            AddColumn("dbo.Follows", "ArtistId", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Follows", new[] { "UserId", "ArtistId" });
            CreateIndex("dbo.Follows", "User_Id");
            CreateIndex("dbo.Follows", "Artist_Id");
            AddForeignKey("dbo.Follows", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
