using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

//TODO: Make this namespace match your project name
namespace MIS333K_FinalProject.Models
{
    public class AppUser : IdentityUser
    {
        //TODO: Add custom user fields - first name is included as an example
        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Display(Name = "Middle Initial")]
        [StringLength(1, MinimumLength = 1)]
        public String? MiddleInitial { get; set; }

        [Required]
        [Display(Name = "Address")]
        public String Address { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [StringLength(5, MinimumLength = 5)]
        public String ZipCode { get; set; }

        [Display(Name = "Social Security Number")]
        public String? SSN { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/1900", "12/31/2006", ErrorMessage = "You must be at least 18 years of age.")]
        public DateTime Birthday { get; set; }

        [Required]
        public bool HireStatus { get; set; }
        

        // Navigational Properties
        public virtual List<Reservation> Reservations { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<Property> Properties { get; set; }

    }
}
