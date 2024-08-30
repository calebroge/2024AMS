using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2024AMS.Models
{
    public partial class UserAsset
    {
        public int UserAssetID { get; set; }

        [Display(Name = "Asset")]
        [Required(ErrorMessage = "Please select an asset.")] // Create the error message and required attribute for the property.
        public int? AssetID { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "Please select a user.")]
        public int? UserID { get; set; }

        [Display(Name = "Checked Out Date")]
        [Required(ErrorMessage = "Please enter a checked out date.")] // Create the error message and required attribute for the property.
        public DateTime? CheckedOutDate { get; set; }

        [Display(Name = "Checked In Date")]
        public DateTime? CheckedInDate { get; set; }

        public virtual Asset? Asset { get; set; }
        public virtual User? User { get; set; }
    }
}
