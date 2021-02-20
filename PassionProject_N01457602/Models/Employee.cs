using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_N01457602.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        //An employee can serve more than 1 customers
        public ICollection<Customer> Customers { get; set; }
    }
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        [DisplayName ("Employee Name")]
        public string EmployeeName { get; set; }
        [DisplayName("Employee Email")]
        public string EmployeeEmail { get; set; }
    }
}
