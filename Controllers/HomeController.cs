using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS333K_FinalProject.DAL;

namespace MIS333K_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context; // Declare _context as a class-level variable

        // Inject the DbContext via the constructor
        public HomeController(AppDbContext context)
        {
            _context = context; // Initialize _context
        }

        public IActionResult Index()
        {
            return RedirectToAction("HostDash", "Home");
        }


        [Authorize(Roles = "Customer")]
        public IActionResult CustomerDash()
        {
            // Get logged-in user's email
            var customerEmail = User.Identity.Name;

            // Fetch the customer using the email
            var customer = _context.Users.FirstOrDefault(u => u.Email == customerEmail);

            // Get the customer's full name or fallback to "Customer"
            var customerName = customer != null ? $"{customer.FirstName} {customer.LastName}" : "Customer";
            ViewData["CustomerName"] = customerName;

            // Fetch only confirmed reservations for the logged-in customer, excluding cancelled ones and those with a ConfirmationNumber of 0
            var reservations = _context.Reservations
                                       .Include(r => r.Property)
                                       .Include(r => r.Customer)
                                       .Where(r => r.Customer.Email == customerEmail)
                                       .Where(r => r.ConfirmationNumber > 0) // Ensure the reservation is confirmed (ConfirmationNumber > 0)
                                       .OrderBy(r => r.StartDate)
                                       .ToList();

            // Fetch all reviews associated with the customer's properties
            var reviews = _context.Reviews
                                  .Where(r => r.Customer.Email == customerEmail)
                                  .ToList();

            // Pass the data to the view
            ViewBag.Reservations = reservations;
            ViewBag.Reviews = reviews;

            return View(reservations); // We still need to pass the model to the view
        }





        [Authorize(Roles = "Host")]
        public IActionResult HostDash()
        {
            var hostEmail = User.Identity.Name; // Get logged-in user's email

            // Fetch the host using the email to get the first and last name
            var host = _context.Users.FirstOrDefault(u => u.Email == hostEmail);

            // Get the host's full name (concatenate first and last name)
            var hostName = host != null ? host.FirstName + " " + host.LastName : "Host";

            // Store the host’s name in ViewData so we can use it in the view
            ViewData["HostName"] = hostName;

            // Fetch the properties owned by the host
            var properties = _context.Properties
                         .Where(p => p.Host.Email == hostEmail)
                         .Include(p => p.Reviews) // Include Reviews explicitly
                         .ToList();

            return View(properties); // Return the view with the list of properties
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDash()
        {
            var adminEmail = User.Identity.Name; // Get logged-in admin's email

            // Fetch the admin using the email to get their first and last name
            var admin = _context.Users.FirstOrDefault(u => u.Email == adminEmail);

            // Get the admin's full name (concatenate first and last name)
            var adminName = admin != null ? admin.FirstName + " " + admin.LastName : "Admin";

            var properties = _context.Properties
                         .Include(p => p.Reviews) // Include Reviews explicitly
                         .ToList();
            // Store the admin’s name in ViewData so we can use it in the view
            ViewData["AdminName"] = adminName;

            return View(properties);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

    }
}
