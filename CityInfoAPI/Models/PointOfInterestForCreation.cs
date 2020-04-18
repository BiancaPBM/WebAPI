using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Models
{
    public class PointOfInterestForCreation
    { [Required (ErrorMessage ="You should have provided a name!")]
      [MaxLength(50)]
      public string Name { get; set; }


      [Required(ErrorMessage = "You should have provided  a description!")]
      [MaxLength(100)]
      public string Description { get; set; }



    }
}
