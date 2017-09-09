namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGroups", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroups", "Group_Id", "dbo.Groups");
            DropIndex("dbo.UserGroups", new[] { "User_Id" });
            DropIndex("dbo.UserGroups", new[] { "Group_Id" });
            AddColumn("dbo.Groups", "GroupPictureUrl", c => c.String());
            AddColumn("dbo.Groups", "GroupInfo", c => c.String());
            AddColumn("dbo.Groups", "Privacy", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "OrganizerId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            CreateIndex("dbo.Groups", "OrganizerId");
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            AddForeignKey("dbo.Groups", "OrganizerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups", "Id");
            DropTable("dbo.UserGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Group_Id });
            
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "OrganizerId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropIndex("dbo.Groups", new[] { "OrganizerId" });
            DropColumn("dbo.AspNetUsers", "Group_Id");
            DropColumn("dbo.Groups", "OrganizerId");
            DropColumn("dbo.Groups", "Privacy");
            DropColumn("dbo.Groups", "GroupInfo");
            DropColumn("dbo.Groups", "GroupPictureUrl");
            CreateIndex("dbo.UserGroups", "Group_Id");
            CreateIndex("dbo.UserGroups", "User_Id");
            AddForeignKey("dbo.UserGroups", "Group_Id", "dbo.Groups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserGroups", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
