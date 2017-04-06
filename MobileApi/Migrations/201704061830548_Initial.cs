namespace MobileApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PumaImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PumaId = c.Int(nullable: false),
                        ImageFilename = c.String(),
                        Credit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pumas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pumas");
            DropTable("dbo.PumaImages");
        }
    }
}
