namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertedSomeChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "DisplayPicture_Url", "dbo.ProfilePictures");
            DropIndex("dbo.AspNetUsers", new[] { "DisplayPicture_Url" });
            RenameColumn(table: "dbo.AspNetUsers", name: "DisplayPicture_Url", newName: "DisplayPicture_Id");
            DropPrimaryKey("dbo.ProfilePictures");
            AddColumn("dbo.ProfilePictures", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AspNetUsers", "DisplayPicture_Id", c => c.Int());
            AlterColumn("dbo.ProfilePictures", "Url", c => c.String(nullable: false));
            AddPrimaryKey("dbo.ProfilePictures", "Id");
            CreateIndex("dbo.AspNetUsers", "DisplayPicture_Id");
            AddForeignKey("dbo.AspNetUsers", "DisplayPicture_Id", "dbo.ProfilePictures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DisplayPicture_Id", "dbo.ProfilePictures");
            DropIndex("dbo.AspNetUsers", new[] { "DisplayPicture_Id" });
            DropPrimaryKey("dbo.ProfilePictures");
            AlterColumn("dbo.ProfilePictures", "Url", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "DisplayPicture_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.ProfilePictures", "Id");
            AddPrimaryKey("dbo.ProfilePictures", "Url");
            RenameColumn(table: "dbo.AspNetUsers", name: "DisplayPicture_Id", newName: "DisplayPicture_Url");
            CreateIndex("dbo.AspNetUsers", "DisplayPicture_Url");
            AddForeignKey("dbo.AspNetUsers", "DisplayPicture_Url", "dbo.ProfilePictures", "Url");
        }
    }
}
