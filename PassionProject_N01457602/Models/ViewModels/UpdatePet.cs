﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_N01457602.Models.ViewModels
{
    public class UpdatePet
    {
        public PetDto pet { get; set; }
        public IEnumerable<CustomerDto> allcustomers { get; set; }
    }
}