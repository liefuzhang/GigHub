namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendanceModified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "ApplicationUser_Id" });
            DropPrimaryKey("dbo.Attendances");
            DropColumn("dbo.Attendances", "AttendeeId");
            RenameColumn(table: "dbo.Attendances", name: "ApplicationUser_Id", newName: "AttendeeId");
            AlterColumn("dbo.Attendances", "AttendeeId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            CreateIndex("dbo.Attendances", "AttendeeId");
            AddForeignKey("dbo.Attendances", "GigId", "dbo.Gigs", "Id");
            AddForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropPrimaryKey("dbo.Attendances");
            AlterColumn("dbo.Attendances", "AttendeeId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Attendances", name: "AttendeeId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Attendances", "AttendeeId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            CreateIndex("dbo.Attendances", "ApplicationUser_Id");
            AddForeignKey("dbo.Attendances", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Attendances", "GigId", "dbo.Gigs", "Id", cascadeDelete: true);
        }
    }
}
