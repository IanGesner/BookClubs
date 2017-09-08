namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renaming1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserUsers", newName: "Friends");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Friends", newName: "UserUsers");
        }
    }
}
