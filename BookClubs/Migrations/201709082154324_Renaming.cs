namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renaming : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserGroups", newName: "UserGroups");
            RenameTable(name: "dbo.ApplicationUserApplicationUsers", newName: "UserUsers");
            RenameColumn(table: "dbo.UserGroups", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.UserUsers", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.UserUsers", name: "ApplicationUser_Id1", newName: "User_Id1");
            RenameIndex(table: "dbo.UserUsers", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.UserUsers", name: "IX_ApplicationUser_Id1", newName: "IX_User_Id1");
            RenameIndex(table: "dbo.UserGroups", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserGroups", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.UserUsers", name: "IX_User_Id1", newName: "IX_ApplicationUser_Id1");
            RenameIndex(table: "dbo.UserUsers", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.UserUsers", name: "User_Id1", newName: "ApplicationUser_Id1");
            RenameColumn(table: "dbo.UserUsers", name: "User_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.UserGroups", name: "User_Id", newName: "ApplicationUser_Id");
            RenameTable(name: "dbo.UserUsers", newName: "ApplicationUserApplicationUsers");
            RenameTable(name: "dbo.UserGroups", newName: "ApplicationUserGroups");
        }
    }
}
