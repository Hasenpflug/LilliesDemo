namespace LilliesDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        LilliesDelivered = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deliveries", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Deliveries", new[] { "CustomerID" });
            DropTable("dbo.Deliveries");
            DropTable("dbo.Customers");
        }
    }
}
