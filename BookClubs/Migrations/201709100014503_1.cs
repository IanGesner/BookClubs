namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 64),
                        LastName = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Isbn = c.String(nullable: false, maxLength: 13),
                        Title = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Isbn);
            
            CreateTable(
                "dbo.GroupEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 128),
                        City = c.String(nullable: false, maxLength: 64),
                        State = c.String(nullable: false, maxLength: 64),
                        ZipCode = c.String(nullable: false, maxLength: 5),
                        DateTime = c.DateTime(nullable: false),
                        BookId = c.String(nullable: false, maxLength: 13),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        City = c.String(),
                        State = c.String(),
                        GroupPictureUrl = c.String(nullable: false),
                        GroupInfo = c.String(maxLength: 1024),
                        Privacy = c.Int(nullable: false),
                        OrganizerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OrganizerId)
                .Index(t => t.OrganizerId);
            
            CreateTable(
                "dbo.GroupInvitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(maxLength: 1024),
                        TimeStamp = c.DateTime(nullable: false),
                        GroupId = c.Int(nullable: false),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        RecipientId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.GroupId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 64),
                        LastName = c.String(nullable: false, maxLength: 64),
                        Biography = c.String(nullable: false, maxLength: 1024),
                        ProfilePictureUrl = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GroupWallPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 1024),
                        TimeStamp = c.DateTime(nullable: false),
                        PosterId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PosterId, cascadeDelete: true)
                .Index(t => t.PosterId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 1024),
                        TimeStamp = c.DateTime(nullable: false),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        RecipientId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId);
            
            CreateTable(
                "dbo.GroupRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(maxLength: 1024),
                        TimeStamp = c.DateTime(nullable: false),
                        GroupId = c.Int(nullable: false),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        RecipientId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.GroupId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_Isbn = c.String(nullable: false, maxLength: 13),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Isbn, t.Author_Id })
                .ForeignKey("dbo.Books", t => t.Book_Isbn, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Book_Isbn)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        User_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_Id, t.User_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.User_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GroupEvents", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "OrganizerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupInvitations", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupInvitations", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupRequests", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupRequests", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupRequests", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.FriendRequests", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupWallPosts", "PosterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupWallPosts", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Friends", "User_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupInvitations", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupEvents", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_Isbn", "dbo.Books");
            DropIndex("dbo.GroupUsers", new[] { "User_Id" });
            DropIndex("dbo.GroupUsers", new[] { "Group_Id" });
            DropIndex("dbo.Friends", new[] { "User_Id1" });
            DropIndex("dbo.Friends", new[] { "User_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_Isbn" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.GroupRequests", new[] { "RecipientId" });
            DropIndex("dbo.GroupRequests", new[] { "SenderId" });
            DropIndex("dbo.GroupRequests", new[] { "GroupId" });
            DropIndex("dbo.FriendRequests", new[] { "RecipientId" });
            DropIndex("dbo.FriendRequests", new[] { "SenderId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.GroupWallPosts", new[] { "GroupId" });
            DropIndex("dbo.GroupWallPosts", new[] { "PosterId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.GroupInvitations", new[] { "RecipientId" });
            DropIndex("dbo.GroupInvitations", new[] { "SenderId" });
            DropIndex("dbo.GroupInvitations", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "OrganizerId" });
            DropIndex("dbo.GroupEvents", new[] { "GroupId" });
            DropIndex("dbo.GroupEvents", new[] { "BookId" });
            DropTable("dbo.GroupUsers");
            DropTable("dbo.Friends");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.GroupRequests");
            DropTable("dbo.FriendRequests");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.GroupWallPosts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.GroupInvitations");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupEvents");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
