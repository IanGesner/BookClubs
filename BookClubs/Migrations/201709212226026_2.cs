namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileDetailsViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Biography = c.String(),
                        ProfilePictureUrl = c.String(),
                        Public = c.Boolean(nullable: false),
                        Friend = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfileListViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfilePictureUrl = c.String(),
                        Biography = c.String(),
                        ProfileDetailsViewModel_Id = c.String(maxLength: 128),
                        PublicDetailsViewModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfileDetailsViewModels", t => t.ProfileDetailsViewModel_Id)
                .ForeignKey("dbo.PublicDetailsViewModels", t => t.PublicDetailsViewModel_Id)
                .Index(t => t.ProfileDetailsViewModel_Id)
                .Index(t => t.PublicDetailsViewModel_Id);
            
            CreateTable(
                "dbo.GroupListItemViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        GroupState = c.String(),
                        GroupCity = c.String(),
                        CurrentBookTitle = c.String(),
                        MemberCount = c.String(),
                        ProfileDetailsViewModel_Id = c.String(maxLength: 128),
                        PublicDetailsViewModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfileDetailsViewModels", t => t.ProfileDetailsViewModel_Id)
                .ForeignKey("dbo.PublicDetailsViewModels", t => t.PublicDetailsViewModel_Id)
                .Index(t => t.ProfileDetailsViewModel_Id)
                .Index(t => t.PublicDetailsViewModel_Id);
            
            CreateTable(
                "dbo.PublicDetailsViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupListItemViewModels", "PublicDetailsViewModel_Id", "dbo.PublicDetailsViewModels");
            DropForeignKey("dbo.ProfileListViewModels", "PublicDetailsViewModel_Id", "dbo.PublicDetailsViewModels");
            DropForeignKey("dbo.GroupListItemViewModels", "ProfileDetailsViewModel_Id", "dbo.ProfileDetailsViewModels");
            DropForeignKey("dbo.ProfileListViewModels", "ProfileDetailsViewModel_Id", "dbo.ProfileDetailsViewModels");
            DropIndex("dbo.GroupListItemViewModels", new[] { "PublicDetailsViewModel_Id" });
            DropIndex("dbo.GroupListItemViewModels", new[] { "ProfileDetailsViewModel_Id" });
            DropIndex("dbo.ProfileListViewModels", new[] { "PublicDetailsViewModel_Id" });
            DropIndex("dbo.ProfileListViewModels", new[] { "ProfileDetailsViewModel_Id" });
            DropTable("dbo.PublicDetailsViewModels");
            DropTable("dbo.GroupListItemViewModels");
            DropTable("dbo.ProfileListViewModels");
            DropTable("dbo.ProfileDetailsViewModels");
        }
    }
}
