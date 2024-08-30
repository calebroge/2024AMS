using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2024AMS.Models
{
    public partial class Asset
    {
        public Asset()
        {
            UserAsset = new HashSet<UserAsset>();
        }

        public int AssetID { get; set; }

        [Display(Name = "Asset Category")]
        [Required(ErrorMessage = "Please select a category.")] // Create the error message and required attribute for the property.
        public int? AssetCategoryID { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "Please select a manufacturer.")] // Create the error message and required attribute for the property.
        public int? ManufacturerID { get; set; }

        [Display(Name = "Operating System")]
        public int? OperatingSystemID { get; set; }

        [Display(Name = "Asset")]
        [Required(ErrorMessage = "Please enter an asset name.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? Asset1 { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Please enter a model.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? Model { get; set; }

        [Display(Name = "Service Tag")]
        [StringLength(50)] // Set the string length maximum to 50.
        public string? ServiceTag { get; set; }

        [Display(Name = "Serial Number")]
        [Required(ErrorMessage = "Please enter a serial number.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? SerialNumber { get; set; }

        [Display(Name = "MAC Address")]
        [StringLength(17)] // Set the string length maximum to 20.
        public string? MACAddress { get; set; }

        [Display(Name = "Barcode")]
        [Required(ErrorMessage = "Please enter a barcode.")] // Create the error message and required attribute for the property.
        [StringLength(13)] // Set the string length maximum to 13.
        public string? Barcode { get; set; }

        [Display(Name = "Warranty Date")]
        [Required(ErrorMessage = "Please enter a warranty date.")] // Create the error message and required attribute for the property.
        public DateTime? WarrantyDate { get; set; }

        [Display(Name = "Purchase Date")]
        [Required(ErrorMessage = "Please enter a purchase date.")] // Create the error message and required attribute for the property.
        public DateTime? PurchaseDate { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        public virtual AssetCategory? AssetCategory { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual OperatingSystem? OperatingSystem { get; set; }
        public virtual ICollection<UserAsset> UserAsset { get; set; }
    }
}
