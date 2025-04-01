using System.ComponentModel.DataAnnotations;

namespace MIS333K_FinalProject.Models
{
    public enum ResStatus { Valid, Cancelled }
    public class Reservation
	{
        // --------------------------------------------------------------------
        // Fields c
        public const Decimal TAX_RATE = .07m;


        // --------------------------------------------------------------------
        // Primary key
        [Key]
		[Required]
		public Int32 ReservationID { get; set; }
        // populate using utilities


        // --------------------------------------------------------------------
        // Properties
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Number of Guests Attending")]
        public Int32 NumOfGuests { get; set; }

        [Required]
        [Display(Name = "Weekday Price")]
        public Decimal WeekdayPrice { get; set; }
        
        [Required]
        [Display(Name = "Weekend Price")]
        public Decimal WeekendPrice { get; set; }

        [Required]
        [Display(Name = "Cleaning Fee")]
        public Decimal CleaningFee { get; set; }

        [Required]
        [Display(Name = "Discount Rate")]
        public Decimal DiscountRate { get; set; }

        [Required]
        [Range(21901, int.MaxValue, ErrorMessage = "Confirmation Number must be 21901 or Greater")]
        [Display(Name = "Confirmation Number")]
        public Int32 ConfirmationNumber { get; set; }
		
        [Required]
        [Display(Name = "Reservation Status")]
        public ResStatus ReservationStatus { get; set; }


        // Calculated Property
        [Display(Name = "Total")]
        public Decimal Total
        {
            get
            {
                // Calculate the number of days between start and end date
                TimeSpan stayDuration = EndDate - StartDate;
                int totalDays = stayDuration.Days;


                int weekdays = 0;
                int weekends = 0;

                for (int i = 0; i <= totalDays; i++) // Adjust loop to include the last day
                {
                    DayOfWeek day = StartDate.AddDays(i).DayOfWeek;
                    if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
                        weekends++;
                    else
                        weekdays++;
                }

                // Calculate the base total
                Decimal baseTotal = (WeekdayPrice * weekdays) + (WeekendPrice * weekends) + CleaningFee;

                // Apply discount if eligible
                if (Property != null && totalDays >= Property.MinNightsForDiscount)
                {
                    baseTotal -= baseTotal * DiscountRate;
                }

                // Apply tax
                Decimal totalWithTax = baseTotal + (baseTotal * TAX_RATE);

                return totalWithTax;
            }
        }



        // --------------------------------------------------------------------
        // Navigational Properties
        public virtual Property Property { get; set; }

        public virtual AppUser Customer { get; set; }

    }

}

