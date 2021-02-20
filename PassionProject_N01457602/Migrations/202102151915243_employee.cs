namespace PassionProject_N01457602.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        EmployeeEmail = c.String(),
                        Customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID)
                .Index(t => t.Customer_CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Customer_CustomerID", "dbo.Customers");
            DropIndex("dbo.Employees", new[] { "Customer_CustomerID" });
            DropTable("dbo.Employees");
        }
    }
}
