using System.ComponentModel.DataAnnotations;

namespace MIS333K_FinalProject.Models
{
    public enum DisStatus { NoDispute, Disputed, ValidDispute, InvalidDispute }
    public class Review
    {
        // --------------------------------------------------------------------
        // Primary Key
        [Key]
        public int ReviewID { get; set; }
        // populate using utilities


        // --------------------------------------------------------------------
        // Properties
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        [Display(Name = "Rating")]
        public Int32 Rating { get; set; }
   

        [MaxLength(280)]
        [Display(Name = "Review Text")]
        public String? ReviewText { get; set; }

        [Display(Name = "Host Comments")]
        public String? HostComments { get; set; }

        [Required]
        [Display(Name = "Dispute Status")]
        public DisStatus DisputeStatus { get; set; }

        [Display(Name = "Property Number")]
        public int PropertyNumber { get; set; }

        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }

        // --------------------------------------------------------------------
        // Navigation properties
        public virtual AppUser Customer { get; set; }

        public virtual Property Property { get; set; }
    }
}
