using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2024AMS.Models
{
    public partial class AssetCategory
    {
        public AssetCategory()
        {
            Asset = new HashSet<Asset>();
        }

        public int AssetCategoryID { get; set; }

        [Display(Name = "Asset Category")]
        [Required(ErrorMessage = "Please enter a category.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? AssetCategory1 { get; set; }

        public virtual ICollection<Asset> Asset { get; set; }
    }
}
