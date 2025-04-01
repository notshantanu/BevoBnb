using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;

namespace MIS333K_FinalProject.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to inject AppDbContext
        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        // Default Index method
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminReport(DateTime? startDate, DateTime? endDate)
        {
            // Initialize the ViewModel
            var viewModel = new AdminReportViewModel();

            // Check if a date range is provided, otherwise use the entire range
            if (startDate.HasValue && endDate.HasValue)
            {
                // Fetch data based on the provided date range
                var reservations = _context.Reservations
                    .Where(r => r.StartDate >= startDate.Value && r.EndDate <= endDate.Value)
                    .ToList();

                // Calculate total commission earned (assuming commission is part of Reservation)
                viewModel.TotalCommissionEarned = reservations.Sum(r => r.Total * 0.10m); // Example: 10% commission on total
                viewModel.TotalReservations = reservations.Count;

                // Get total properties (based on the unique property IDs in reservations)
                viewModel.TotalProperties = _context.Properties.Count();
            }
            else
            {
                // If no date range is provided, fetch all data
                var reservations = _context.Reservations.ToList();

                viewModel.TotalCommissionEarned = reservations.Sum(r => r.Total * 0.10m); // Example: 10% commission
                viewModel.TotalReservations = reservations.Count;

                // Get total properties (based on the unique property IDs in reservations)
                viewModel.TotalProperties = _context.Properties.Count();
            }

            // Return the populated view model to the view
            return View(viewModel);
        }



        // Method for Host Report
        public IActionResult HostReport()
        {
            return View();
        }
    }
}