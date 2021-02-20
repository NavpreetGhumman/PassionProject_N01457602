namespace PassionProject_N01457602.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeTableUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Customer_CustomerID", "dbo.Customers");
            DropIndex("dbo.Employees", new[] { "Customer_CustomerID" });
            CreateTable(
                "dbo.EmployeeCustomers",
                c => new
                    {
                        Employee_EmployeeID = c.Int(nullable: false),
                        Customer_CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Employee_EmployeeID, t.Customer_CustomerID })
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID, cascadeDelete: true)
                .Index(t => t.Employee_EmployeeID)
                .Index(t => t.Customer_CustomerID);
            
            DropColumn("dbo.Employees", "Customer_CustomerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Customer_CustomerID", c => c.Int());
            DropForeignKey("dbo.EmployeeCustomers", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.EmployeeCustomers", "Employee_EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeCustomers", new[] { "Customer_CustomerID" });
            DropIndex("dbo.EmployeeCustomers", new[] { "Employee_EmployeeID" });
            DropTable("dbo.EmployeeCustomers");
            CreateIndex("dbo.Employees", "Customer_CustomerID");
            AddForeignKey("dbo.Employees", "Customer_CustomerID", "dbo.Customers", "CustomerID");
        }
    }
}
