namespace BookClubs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewGroupModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "City", c => c.String());
            AddColumn("dbo.Groups", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "State");
            DropColumn("dbo.Groups", "City");
        }
    }
}
