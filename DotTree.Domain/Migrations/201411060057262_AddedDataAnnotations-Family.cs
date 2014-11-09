namespace DotTree.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataAnnotationsFamily : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Families", "FamilyName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Families", "FamilyName", c => c.String());
        }
    }
}
