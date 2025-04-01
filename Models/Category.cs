using System;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_FinalProject.Models
{
	public class Category
	{
        // --------------------------------------------------------------------
        // Primary Key
        [Key]
        [Required]
        public Int32 CategoryId { get; set; }
        // populate using utilities


        // --------------------------------------------------------------------
         // Properties
        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only letters and spaces are allowed")]
        public string CategoryName { get; set; }


        // --------------------------------------------------------------------
        // Navigation Properties
        public virtual List<Property> Properties { get; set; }
    }
}

