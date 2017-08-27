namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProfilePicModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupEvents", "Book_ISBN", "dbo.Books");
            DropIndex("dbo.GroupEvents", new[] { "Book_ISBN" });
            DropIndex("dbo.BookAuthors", new[] { "Book_ISBN" });
            CreateTable(
                "dbo.ProfilePictures",
                c => new
                    {
                        Url = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Url)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "DisplayPicture_Url", c => c.String(maxLength: 128));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "City", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "State", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "ZipCode", c => c.String(nullable: false));
            AlterColumn("dbo.GroupEvents", "Book_Isbn", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            CreateIndex("dbo.GroupEvents", "Book_Isbn");
            CreateIndex("dbo.AspNetUsers", "DisplayPicture_Url");
            CreateIndex("dbo.BookAuthors", "Book_Isbn");
            AddForeignKey("dbo.AspNetUsers", "DisplayPicture_Url", "dbo.ProfilePictures", "Url");
            AddForeignKey("dbo.GroupEvents", "Book_Isbn", "dbo.Books", "Isbn", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupEvents", "Book_Isbn", "dbo.Books");
            DropForeignKey("dbo.ProfilePictures", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "DisplayPicture_Url", "dbo.ProfilePictures");
            DropIndex("dbo.BookAuthors", new[] { "Book_Isbn" });
            DropIndex("dbo.ProfilePictures", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "DisplayPicture_Url" });
            DropIndex("dbo.GroupEvents", new[] { "Book_Isbn" });
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.GroupEvents", "Book_Isbn", c => c.String(maxLength: 128));
            AlterColumn("dbo.GroupEvents", "ZipCode", c => c.String());
            AlterColumn("dbo.GroupEvents", "State", c => c.String());
            AlterColumn("dbo.GroupEvents", "City", c => c.String());
            AlterColumn("dbo.GroupEvents", "Address", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
            DropColumn("dbo.AspNetUsers", "DisplayPicture_Url");
            DropTable("dbo.ProfilePictures");
            CreateIndex("dbo.BookAuthors", "Book_ISBN");
            CreateIndex("dbo.GroupEvents", "Book_ISBN");
            AddForeignKey("dbo.GroupEvents", "Book_ISBN", "dbo.Books", "ISBN");
        }
    }
}
