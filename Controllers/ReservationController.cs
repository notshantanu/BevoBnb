using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MIS333K_FinalProject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservation
        [Authorize(Roles = "Admin,Host")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.Include(r => r.Property).Include(r => r.Customer).ToListAsync());
        }

        // GET CREATE RESERVATION
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(int propertyId)
        {
            Console.WriteLine($"PropertyId received in Create GET: {propertyId}"); // Debugging log

            var property = await _context.Properties.FirstOrDefaultAsync(p => p.PropertyId == propertyId);
            if (property == null)
            {
                return NotFound("Property not found.");
            }

            ViewBag.Property = property;
            return View(new Reservation { Property = property });
        }

        // POST CREATE RESERVATION
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([Bind("StartDate,EndDate,NumOfGuests")] Reservation reservation, int propertyId)
        {
            Console.WriteLine($"PropertyId received in Create POST: {propertyId}"); // Debugging log

            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null)
            {
                Console.WriteLine("Property not found in database."); // Debugging log
                ModelState.AddModelError("", "Invalid Property ID. Property not found.");
                return View(reservation);
            }

            Console.WriteLine($"Property retrieved: {property.Street}, {property.City}"); // Debugging log

            // Add date validation for past dates
            if (reservation.StartDate <= DateTime.Now.Date)
            {
                ModelState.AddModelError("StartDate", "Start Date must be a future date.");
                ViewBag.Property = property; // Ensure the property data is available on the view
                return View(reservation);
            }

            if (reservation.EndDate < DateTime.Now.Date)
            {
                ModelState.AddModelError("EndDate", "End Date must be a future date.");
                ViewBag.Property = property; // Ensure the property data is available on the view
                return View(reservation);
            }

            // Ensure StartDate is before EndDate
            if (reservation.StartDate > reservation.EndDate)
            {
                ModelState.AddModelError("EndDate", "End Date must be later than Start Date.");
                ViewBag.Property = property; // Ensure the property data is available on the view
                return View(reservation);
            }

            // Check if NumOfGuests exceeds the property limit
            if (reservation.NumOfGuests > property.GuestsAllowed)
            {
                ModelState.AddModelError("NumOfGuests", $"The number of guests cannot exceed the limit of {property.GuestsAllowed} for this property.");
                ViewBag.Property = property; // Ensure the property data is available on the view
                return View(reservation);
            }

            var user = await _userManager.GetUserAsync(User);

            // Check for overlapping reservations
            bool isOverlap = await _context.Reservations
                .Where(r => r.Customer.Id == user.Id)
                .AnyAsync(r =>
                    r.ReservationStatus == ResStatus.Valid && // Ensure only valid reservations are checked
                    (reservation.StartDate < r.EndDate && reservation.EndDate > r.StartDate)
                );
            
            bool isOverlapRes = await _context.Reservations
                .Where(r => r.Property.PropertyId == property.PropertyId)
                .AnyAsync(r =>
                    r.ReservationStatus == ResStatus.Valid && // Ensure only valid reservations are checked
                    (reservation.StartDate < r.EndDate && reservation.EndDate > r.StartDate)
                );

            if (isOverlap)
            {
                Console.WriteLine("Overlapping reservation detected."); // Debugging log
                ModelState.AddModelError("StartDate", "You already have a reservation for this time period.");
                ViewBag.Property = property;
                return View(reservation);
            }
            
            if (isOverlapRes)
            {
                Console.WriteLine("Overlapping reservation detected."); // Debugging log
                ModelState.AddModelError("StartDate", "There is already a reservation for this time period");
                ViewBag.Property = property;
                return View(reservation);
            }

            // Proceed to add the reservation
            reservation.Property = property;
            reservation.Customer = user;
            reservation.ReservationStatus = ResStatus.Valid;
            reservation.WeekdayPrice = property.WeekdayPrice;
            reservation.WeekendPrice = property.WeekendPrice;
            reservation.CleaningFee = property.CleaningFee;
            reservation.DiscountRate = property.DiscountRate;

            _context.Add(reservation);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Reservation added successfully to your cart!";
            return RedirectToAction(nameof(Create), new { propertyId }); // Stay on the same page with a success message // Redirect to cart instead of checkout
        }



        // GET: View Cart
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized("You need to be logged in to view your cart.");
            }

            var cartItems = await _context.Reservations
                .Where(r => r.Customer.Id == user.Id && r.ReservationStatus == ResStatus.Valid)
                .Where(r => r.ConfirmationNumber == 0)
                .Include(r => r.Property)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return View("Cart"); // Optional: Create a view to handle an empty cart
            }

            return View(cartItems); // Pass the valid reservations to the cart view
        }


        // POST: Move to Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MoveToCheckout(int reservationId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized("You need to be logged in to proceed.");
            }

            var reservation = await _context.Reservations
                .Include(r => r.Property)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReservationID == reservationId);

            if (reservation == null || reservation.Customer.Id != user.Id || reservation.ReservationStatus != ResStatus.Valid)
            {
                return NotFound("Reservation not found or unauthorized access.");
            }

            // Mark reservation as ready for checkout
            reservation.ReservationStatus = ResStatus.Valid; // Update status as needed
            _context.Update(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Checkout), new { id = reservationId });
        }

        // Post: Remove Reservations
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Remove(int id)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationID == id);

            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (reservation.Customer?.Id != user.Id)
            {
                return Unauthorized();
            }

            // Remove the reservation
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Reservation removed successfully.";
            return RedirectToAction(nameof(Cart));
        }



        /// GET: Reservations/Checkout/5
        public async Task<IActionResult> Checkout(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Property)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.ReservationID == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }


        private int GenerateNextConfirmationNumber()
        {
            // If there are existing reservations, find the max ConfirmationNumber and add 1
            return _context.Reservations.Any()
                ? _context.Reservations.Max(r => r.ConfirmationNumber) + 1
                : 21900;
        }



        // POST: Reservations/Checkout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout([Bind("ReservationID,ConfirmationNumber")] Reservation reservation)
        {
            if (ModelState.IsValid == false)
            {
                try
                {
                    // Retrieve the reservation from the database
                    var existingReservation = await _context.Reservations
                        .Include(r => r.Property)
                        .Include(r => r.Customer)
                        .FirstOrDefaultAsync(r => r.ReservationID == reservation.ReservationID);

                    if (existingReservation == null)
                    {
                        return NotFound();
                    }

                    // Assign a confirmation number and update reservation status
                    existingReservation.ConfirmationNumber = GenerateNextConfirmationNumber();
                    existingReservation.ReservationStatus = ResStatus.Valid;

                    _context.Update(existingReservation);
                    await _context.SaveChangesAsync();

                    // Redirect to the Customer Dashboard
                    return RedirectToAction("CustomerDash", "Home");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(reservation);
        }




        // GET: Reservations/CheckoutConfirmation/5
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Confirmation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Property)
                .FirstOrDefaultAsync(m => m.ReservationID == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (reservation.Customer?.Id != user.Id)
            {
                return Unauthorized();
            }

            ViewBag.ConfirmationNumber = reservation.ConfirmationNumber;
            return View(reservation);
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }


        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Cancel(int resId)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(p => p.ReservationID == resId);

            if (reservation == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (reservation.Customer?.Id != user.Id)
            {
                return Unauthorized();
            }

            reservation.ReservationStatus = ResStatus.Cancelled;

            // Save changes to the database
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Reservation cancelled successfully.";
            return RedirectToAction("CustomerDash", "Home");
        }
    }
}




