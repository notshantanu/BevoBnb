using System;

namespace MIS333K_FinalProject.Models
{
    public class AdminReportViewModel
    {
        /// <summary>
        /// Total commission BevoBnB earned during the selected date range.
        /// </summary>
        public decimal TotalCommissionEarned { get; set; }

        /// <summary>
        /// Total number of reservations during the selected date range.
        /// </summary>
        public int TotalReservations { get; set; }

        /// <summary>
        /// Average commission per reservation during the selected date range.
        /// </summary>
        public decimal AverageCommissionPerReservation
        {
            get
            {
                return TotalReservations > 0 ? TotalCommissionEarned / TotalReservations : 0;
            }
        }

        /// <summary>
        /// Total number of properties on the platform.
        /// </summary>
        public int TotalProperties { get; set; }
    }
}