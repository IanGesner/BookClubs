namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotsOfSchemaChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "DisplayPicture_Id", "dbo.ProfilePictures");
            DropForeignKey("dbo.ProfilePictures", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "DisplayPicture_Id" });
            DropIndex("dbo.ProfilePictures", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "ProfilePictureUrl", c => c.String());
            DropColumn("dbo.AspNetUsers", "DisplayPicture_Id");
            DropTable("dbo.ProfilePictures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfilePictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "DisplayPicture_Id", c => c.Int());
            DropColumn("dbo.AspNetUsers", "ProfilePictureUrl");
            CreateIndex("dbo.ProfilePictures", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "DisplayPicture_Id");
            AddForeignKey("dbo.ProfilePictures", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "DisplayPicture_Id", "dbo.ProfilePictures", "Id");
        }
    }
}
