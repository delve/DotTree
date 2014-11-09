namespace DotTree.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFamilyWithManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonFamilies",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Family_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Family_Id })
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Families", t => t.Family_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.Family_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonFamilies", "Family_Id", "dbo.Families");
            DropForeignKey("dbo.PersonFamilies", "Person_Id", "dbo.People");
            DropIndex("dbo.PersonFamilies", new[] { "Family_Id" });
            DropIndex("dbo.PersonFamilies", new[] { "Person_Id" });
            DropTable("dbo.PersonFamilies");
            DropTable("dbo.Families");
        }
    }
}
