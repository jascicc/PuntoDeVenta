namespace PuntoDeVenta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solvingissue : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.StaticConfigs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StaticConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dollar = c.Single(nullable: false),
                        Name = c.String(nullable: false),
                        RFC = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
