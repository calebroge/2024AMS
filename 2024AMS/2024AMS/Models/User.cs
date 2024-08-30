using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2024AMS.Models
{
    public partial class User
    {
        public User()
        {
            UserAsset = new HashSet<UserAsset>();
        }

        public int UserID { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter a last name.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? LastName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter a first name.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        [StringLength(1)] // Set the string length maximum to 50.
        public string? MiddleInitial { get; set; }

        [Display(Name = "Email Address")]
        [RegularExpression(@"\S+\@\S+\.\S+",
            ErrorMessage = "Please enter an email address of the form: " +
            "aaa@bbb.ccc.")]
        [Required(ErrorMessage = "Please enter a email address.")] // Create the error message and required attribute for the property.
        [StringLength(50)] // Set the string length maximum to 50.
        public string? EmailAddress { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select a status.")] // Create the error message and required attribute for the property.
        public string? Status { get; set; }

        public virtual ICollection<UserAsset> UserAsset { get; set; }
    }
}
