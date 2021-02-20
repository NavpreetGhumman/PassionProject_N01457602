using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_N01457602.Models.ViewModels
{
    public class ShowCustomer
    {
        //Information about the customer
        public CustomerDto customer { get; set; }

        //Information about all pets bought by that customer
        public IEnumerable<PetDto> customerpets { get; set; }

        //Information about all employeess that served to that particular customer
        public IEnumerable<EmployeeDto> customerEmployees{ get; set; }
    }
}