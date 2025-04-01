using System;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_FinalProject.Models
{
    public class Unavailability
    {
        [Key]
        [Required]
        [Display(Name = "Unavailability ID")]
        public int UnavailabilityId { get; set; }

        [Required]
        [Display(Name = "Date Time")]
        public DateTime Date { get; set; }

        [Display(Name = "Property Name")]
        public int PropertyId { get; set; }

        // Navigation Properties
        public virtual Property Property { get; set; }
    }

}

