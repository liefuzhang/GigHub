namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfigurationsToFollowingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
