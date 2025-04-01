using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MIS333K_FinalProject.Models;

namespace MIS333K_FinalProject.ViewModels
{
    public enum GuestRatingOperator
    {
        GreaterThan,
        LessThan
    }

    public class PropertySearchViewModel
    {
        // Property for City search
        [Display(Name = "City")]
        public string? City { get; set; }

        // Property for State search
        [Display(Name = "State")]
        [StringLength(2, ErrorMessage = "State must be a valid two-letter abbreviation.")]
        public string? State { get; set; }

        // Property for Guest Rating search
        [Display(Name = "Guest Rating")]
        [Range(1.0, 5.0, ErrorMessage = "Guest rating must be between 1.0 and 5.0.")]
        public decimal? AverageRating { get; set; }

        [Display(Name = "Guest Rating Operator")]
        public GuestRatingOperator? GuestRatingOperator { get; set; }

        // Property for Minimum Daily Price search
        [Display(Name = "Minimum Daily Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum price cannot be negative.")]
        public decimal? MinDailyPrice { get; set; }

        // Property for Maximum Daily Price search
        [Display(Name = "Maximum Daily Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Maximum price cannot be negative.")]
        public decimal? MaxDailyPrice { get; set; }

        // Property for Category search
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        // Property for Number of Bedrooms search
        [Display(Name = "Number of Bedrooms")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of bedrooms cannot be negative.")]
        public int? Bedrooms { get; set; }

        // Property for Number of Bathrooms search
        [Display(Name = "Number of Bathrooms")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of bathrooms cannot be negative.")]
        public int? Bathrooms { get; set; }

        // Property for Number of Guests search
        [Display(Name = "Number of Guests")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of guests cannot be negative.")]
        public int? Guests { get; set; }

        // Property for Pets Allowed filter
        [Display(Name = "Pets Allowed")]
        public bool? PetsAllowed { get; set; }

        // Property for Free Parking filter
        [Display(Name = "Free Parking")]
        public bool? FreeParking { get; set; }

        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        // List of Categories for the drop-down
        [Display(Name = "Available Categories")]
        public List<Category>? Categories { get; set; }
    }
}
