namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        UserId = c.Byte(nullable: false),
                        ArtistId = c.Byte(nullable: false),
                        Artist_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ArtistId })
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "User_Id" });
            DropIndex("dbo.Followings", new[] { "Artist_Id" });
            DropTable("dbo.Followings");
        }
    }
}
