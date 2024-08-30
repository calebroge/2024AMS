using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2024AMS.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Asset = new HashSet<Asset>();
        }

        public int ManufacturerID { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "Please enter a manufacturer.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? Manufacturer1 { get; set; }

        public virtual ICollection<Asset> Asset { get; set; }
    }
}
