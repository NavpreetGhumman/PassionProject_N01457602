using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_N01457602.Models.ViewModels
{
    public class UpdatePet
    {
        public PetDto pet { get; set; }
        //Needed for a dropdownlist which presents the which customer bought a particular pet
        public IEnumerable<CustomerDto> allcustomers { get; set; }
    }
}