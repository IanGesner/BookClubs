namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.GroupEvents", name: "Book_Isbn", newName: "BookId");
            RenameIndex(table: "dbo.GroupEvents", name: "IX_Book_Isbn", newName: "IX_BookId");
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        RecipientId = c.String(nullable: false, maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.User_Id);
            
            AlterColumn("dbo.Books", "Title", c => c.String());
            AlterColumn("dbo.GroupEvents", "Address", c => c.String());
            AlterColumn("dbo.GroupEvents", "City", c => c.String());
            AlterColumn("dbo.GroupEvents", "State", c => c.String());
            AlterColumn("dbo.GroupEvents", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendRequests", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "RecipientId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendRequests", new[] { "User_Id" });
            DropIndex("dbo.FriendRequests", new[] { "RecipientId" });
            DropIndex("dbo.FriendRequests", new[] { "SenderId" });
            AlterColumn("dbo.GroupEvents", "ZipCode", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "State", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "City", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            DropTable("dbo.FriendRequests");
            RenameIndex(table: "dbo.GroupEvents", name: "IX_BookId", newName: "IX_Book_Isbn");
            RenameColumn(table: "dbo.GroupEvents", name: "BookId", newName: "Book_Isbn");
        }
    }
}
