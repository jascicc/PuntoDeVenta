namespace PuntoDeVenta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSpendingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Spendings",
                c => new
                    {
                        SpendingID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Cost = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SpendingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Spendings");
        }
    }
}
