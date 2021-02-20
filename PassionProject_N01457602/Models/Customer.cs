using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_N01457602.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        //A customer can have many pets
        public ICollection<Pet> Pets { get; set; }
        //A customer can be served by many employees
        public ICollection<Employee> Employees { get; set; }

    }
    public class CustomerDto
    {
        public int CustomerID { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [DisplayName("Customer Email")]
        public string CustomerEmail { get; set; }
        [DisplayName("Customer Phone")]
        public string CustomerPhone { get; set; }
    }
}