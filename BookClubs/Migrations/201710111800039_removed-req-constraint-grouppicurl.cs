namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedreqconstraintgrouppicurl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Groups", "GroupPictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "GroupPictureUrl", c => c.String(nullable: false));
        }
    }
}
