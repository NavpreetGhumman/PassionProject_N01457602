using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_N01457602.Models.ViewModels
{
    public class ShowPet
    {
        public PetDto pet { get; set; }
        //information about the customer who buy particular pet
        public CustomerDto customer { get; set; }
    }
}