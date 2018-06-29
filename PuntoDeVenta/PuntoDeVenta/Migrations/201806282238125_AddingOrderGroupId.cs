namespace PuntoDeVenta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingOrderGroupId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "GroupID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "GroupID");
        }
    }
}
