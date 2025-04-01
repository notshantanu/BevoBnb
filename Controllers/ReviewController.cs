using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;


namespace MIS333K_FinalProject.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public ReviewController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Review
        [Authorize(Roles = "Admin,Host")]
        public async Task<IActionResult> Index(int propertyId)
        {
            // Filter reviews to only include those for the specified property
            var reviews = await _context.Reviews
                .Where(r => r.Property.PropertyId == propertyId)
                .ToListAsync();

            return View(reviews);
        }

        // GET: Review/Details/5
        [Authorize(Roles = "Customer,Host,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }


        // GET: Review/Create
        [Authorize(Roles = "Customer")]
        public IActionResult Create(int propId)
        {
            var property = _context.Properties
                                   .FirstOrDefault(p => p.PropertyId == propId);

            if (property == null)
            {
                return NotFound("Property not found.");
            }

            ViewBag.Property = property; // Set the property in ViewBag
            return View(new Review { PropertyNumber = propId }); // Pass the PropertyNumber to the view
        }


        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([Bind("Rating,ReviewText")] Review review, int propId)
        {
            review.PropertyNumber = propId; // Explicitly set PropertyNumber to match PropertyId

            Console.WriteLine($"Received Property ID: {review.PropertyNumber}");

            var property = await _context.Properties.FindAsync(propId);
            if (property == null)
            {
                Console.WriteLine("Property not found in database."); // Debugging log
                ModelState.AddModelError("", "Invalid Property ID. Property not found.");
                return View(review);
            }

            var user = await _userManager.GetUserAsync(User);

            // Assign the property ID to avoid tracking the full entity
            review.Property = property;
            review.Customer = user;
            review.CustomerEmail = user?.Email;
            review.DisputeStatus = DisStatus.NoDispute;

            _context.Add(review);
            await _context.SaveChangesAsync();


            TempData["Message"] = "Review successfully posted!";

            return RedirectToAction("CustomerDash", "Home");
        }



        // GET: Review/Edit/5
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Edit(int id, [Bind("Rating,ReviewText")] Review review)
        {
            // Retrieve the existing review from the database
            var existingReview = await _context.Reviews
                .Include(r => r.Property)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (existingReview == null)
            {
                return NotFound("Review not found.");
            }

            // Retrieve the logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null || existingReview.Customer?.Id != user.Id)
            {
                return Unauthorized("You are not authorized to edit this review.");
            }

            // Update only the fields allowed for editing
            existingReview.Rating = review.Rating;
            existingReview.ReviewText = review.ReviewText;

            try
            {
                _context.Update(existingReview);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Review updated successfully.";
                return RedirectToAction("CustomerDash", "Home"); // Redirect to the dashboard
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("An error occurred while updating the review.");
            }
        }



        // GET: Review/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }


        // GET: Review/Dispute/5
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> Dispute(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Dispute/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> Dispute(int id, [Bind("HostComments")] Review review)
        {
            // Retrieve the existing review from the database
            var existingReview = await _context.Reviews
                .Include(r => r.Property)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (existingReview == null)
            {
                return NotFound("Review not found.");
            }

            // Retrieve the logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null || existingReview.Property.Host.Id != user.Id)
            {
                return Unauthorized("You are not authorized to edit this review.");
            }

            // Update only the fields allowed for editing
            if (review.HostComments != null)
            {
                existingReview.HostComments = review.HostComments;
            }
            existingReview.DisputeStatus = DisStatus.Disputed;

            try
            {
                _context.Update(existingReview);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Review disputed successfully.";
                return RedirectToAction("HostDash", "Home"); // Redirect to the dashboard
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("An error occurred while disputing the review.");
            }
        }



        // GET: Review/ManageDispute/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageDispute(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/ManageDispute/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageDispute(int id, [Bind("DisputeStatus")] Review review)
        {
            // Retrieve the existing review from the database
            var existingReview = await _context.Reviews
                .Include(r => r.Property)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (existingReview == null)
            {
                return NotFound("Review not found.");
            }

            // Retrieve the logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized("You are not authorized to edit this review.");
            }

            // Update only the fields allowed for editing
            existingReview.DisputeStatus = review.DisputeStatus;

            try
            {
                _context.Update(existingReview);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Review dispute managed successfully.";
                return RedirectToAction("AdminDash", "Home"); // Redirect to the dashboard
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("An error occurred while disputing the review.");
            }
        }
    }
}
