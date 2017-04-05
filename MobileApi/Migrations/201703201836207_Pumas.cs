namespace MobileApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pumas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PumaTypeImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PumaTypeId = c.Int(nullable: false),
                        ImageFilename = c.String(),
                        Credit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PumaTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PumaTypes");
            DropTable("dbo.PumaTypeImages");
        }
    }
}
