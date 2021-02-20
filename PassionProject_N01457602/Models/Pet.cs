using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PassionProject_N01457602.Models
{
    public class Pet
    {
        //This class describes a pet entity.
        //It is used for actually generating a database table
        [Key]
        public int PetID { get; set; }

        public string PetName { get; set; }

        public string PetColour { get; set; }

        public string PetAge { get; set; }

        public string PetBreed { get; set; }

        public int PetHeight { get; set; }

        public string PetLocation { get; set; }
        //A player plays for one team
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }

    //This class can be used to transfer information about a player.
    //also known as a "Data Transfer Object"
    //https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
    //You can think of this class as the 'Model' that was used in 5101.
    //It is simply a vessel of communication
    public class PetDto
    {
        public int PetID { get; set; }
        [DisplayName("Name")]
        public string PetName { get; set; }
        [DisplayName("Colour")]
        public string PetColour { get; set; }
        [DisplayName("Age")]
        public string PetAge { get; set; }
        [DisplayName("Breed")]
        public string PetBreed { get; set; }
        [DisplayName("Height")]
        public int PetHeight { get; set; }
        [DisplayName("Location")]
        public string PetLocation { get; set; }
        [DisplayName("Customer ID ")]
        public int CustomerID { get; set; }
    }
}
