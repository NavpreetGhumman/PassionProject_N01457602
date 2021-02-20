namespace PassionProject_N01457602.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerEmail = c.String(),
                        CustomerPhone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            AddColumn("dbo.Pets", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pets", "CustomerID");
            AddForeignKey("dbo.Pets", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pets", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Pets", new[] { "CustomerID" });
            DropColumn("dbo.Pets", "CustomerID");
            DropTable("dbo.Customers");
        }
    }
}
