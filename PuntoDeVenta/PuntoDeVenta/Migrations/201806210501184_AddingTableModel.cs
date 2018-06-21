namespace PuntoDeVenta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTableModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        TableNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TableId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tables");
        }
    }
}
