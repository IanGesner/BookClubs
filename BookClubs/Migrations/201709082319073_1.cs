namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FriendRequests", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FriendRequests", new[] { "User_Id" });
            DropColumn("dbo.FriendRequests", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendRequests", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FriendRequests", "User_Id");
            AddForeignKey("dbo.FriendRequests", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
