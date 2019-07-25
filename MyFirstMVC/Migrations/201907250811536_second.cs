namespace MyFirstMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Projects", "CategoryId", c => c.Int());
            CreateIndex("dbo.Projects", "CategoryId");
            AddForeignKey("dbo.Projects", "CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Projects", new[] { "CategoryId" });
            DropColumn("dbo.Projects", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
