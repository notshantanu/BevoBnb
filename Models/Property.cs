using System;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_FinalProject.Models
{
    public enum PropStatus { Approved, Unapproved }
    public enum StateAbbr { AL, AK, AZ, AR, CA, CO, CT, DC, DE, FL, GA, HI, ID, IL,
        IN, IA, KS, KY, LA, ME, MD, MA, MI, MN, MS, MO, MT, NE, NV, NH, NJ, NM,
        NY, NC, ND, OH, OK, OR, PA, RI, SC, SD, TN, TX, UT, VT, VA, WA, WV, WI,
        WY }
	public class Property
	{
        // --------------------------------------------------------------------
        // Primary Key
        [Key]
        //[Required]
        //[Range(3001, int.MaxValue, ErrorMessage = "PropertyID must be 3001 or greater")]
        public int PropertyId { get; set; }
        // use annotations for validity errors
        // use viewbag for authorization errors


        // --------------------------------------------------------------------
        // Properties
        [Required]
        [Display(Name = "Street Address")]
        public String Street { get; set; } // Includes Apt Num

        [Required]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required]
        [Display(Name = "State")]
        public StateAbbr State { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip Code must be exactly 5 digits.")]
        [Display(Name = "Zip Code")]
        public String ZipCode { get; set; }

        //[EmailAddress]
        //[Required]
        //[Display(Name = "Host Email")]
        //public String HostEmail { get; set; }

        [Required]
        [Display(Name = "Number of Bedrooms")]
        public Int32 Bedrooms { get; set; }

        [Required]
        [Display(Name = "Number of Bathrooms")]
        public Int32 Bathrooms { get; set; }

        [Required]
        [Display(Name = "Number of Guests Allowed")]
        public Int32 GuestsAllowed { get; set; }

        [Required]
        [Display(Name = "Pets Allowed")]
        public bool PetsAllowed { get; set; }

        [Required]
        [Display(Name = "Free Parking")]
        public bool FreeParking { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Weekday Price")]
        public Decimal WeekdayPrice { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Weekend Price")]
        public Decimal WeekendPrice { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Cleaning Fee")]
        public Decimal CleaningFee { get; set; }

        [Range(0, 100, ErrorMessage = "Discount Rate must be between 0% and 100%.")]
        [Display(Name = "Discount Rate")]
        public Decimal DiscountRate { get; set; }

        [Range(0, 30, ErrorMessage = "Minimum Nights for Discount must be between 0 and 30.")]
        [Display(Name = "Minimum Nights for Discount")]
        public Int32 MinNightsForDiscount { get; set; }

        //[Display(Name = "Unavailable Dates")]
        //public DateTime UnavailableDates { get; set; }

        [Required]
        [Display(Name = "Property Status")]
        public PropStatus PropertyStatus { get; set; }

        // Calculated Property
        [Display(Name = "Average Rating")]
        public Decimal AverageRating
        {
            get
            {
                // Ensure Reviews is not null and contains at least one review
                if (Reviews == null || Reviews.Count == 0)
                {
                    return 0m; // Return 0 if no reviews are available
                }

                // Filter reviews based on the status condition
                var validReviews = Reviews.Where(review =>
                    review.DisputeStatus == DisStatus.NoDispute || review.DisputeStatus == DisStatus.InvalidDispute).ToList();

                // Ensure validReviews has elements to avoid DivideByZeroException
                if (validReviews.Count == 0)
                {
                    return 0m; // Return 0 if no valid reviews meet the condition
                }

                // Use LINQ to calculate the average rating from the filtered list
                return (decimal)validReviews.Average(review => review.Rating);
            }
        }


        // --------------------------------------------------------------------
        // Navigational Properties
        public virtual AppUser? Host { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Reservation>? Reservations { get; set; }

        public virtual List<Review>?  Reviews { get; set; }

        public virtual List<Unavailability>? Unavailabilities { get; set; }

    }
}

