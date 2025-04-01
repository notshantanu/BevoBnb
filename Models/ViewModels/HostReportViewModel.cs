using System.ComponentModel.DataAnnotations;

namespace MIS333K_FinalProject.Models
{
    public class HostReportViewModel
    {
        public string PropertyName { get; set; }
        public decimal TotalStayRevenue { get; set; }  // 90% of the stay revenue
        public decimal TotalCleaningFees { get; set; } // Total cleaning fees collected
        public decimal TotalCombinedRevenue { get; set; } // Stay revenue + Cleaning fees
        public int TotalCompletedReservations { get; set; } // Number of completed reservations
    }
}