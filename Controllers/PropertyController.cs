using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;
using MIS333K_FinalProject.ViewModels;

namespace MIS333K_FinalProject.Controllers
{
    [AllowAnonymous]
    public class PropertyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PropertyController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(
    string city, string state, int? guests,
    decimal? averageRating, bool? petsAllowed, bool? freeParking)
        {
            // Start with a queryable set of properties
            var query = _context.Properties.AsQueryable();

            // Apply filters for database-mapped properties
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(p => EF.Functions.Like(p.City, $"%{city}%"));
            }

            if (!string.IsNullOrEmpty(state))
            {
                if (Enum.TryParse(typeof(StateAbbr), state, out var stateEnum))
                {
                    query = query.Where(p => p.State == (StateAbbr)stateEnum);
                }
            }

            if (guests.HasValue)
            {
                query = query.Where(p => p.GuestsAllowed >= guests.Value);
            }

            if (averageRating.HasValue)
            {
                query = query.Where(p => p.AverageRating >= averageRating.Value);
            }

            if (petsAllowed.HasValue)
            {
                query = query.Where(p => p.PetsAllowed == petsAllowed.Value);
            }

            if (freeParking.HasValue)
            {
                query = query.Where(p => p.FreeParking == freeParking.Value);
            }

            // Retrieve the filtered properties
            var filteredProperties = await query.Include(p => p.Category).ToListAsync();

            // Count all properties for display
            var totalProperties = await _context.Properties.CountAsync();

            // Set ViewBag values for display
            ViewBag.SelectedProperties = filteredProperties.Count;
            ViewBag.AllProperties = totalProperties;

            // Return the view with the filtered list of properties
            return View(filteredProperties);
        }



        [AllowAnonymous]
        public IActionResult Search()
        {
            var viewModel = new PropertySearchViewModel
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Properties
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(m => m.PropertyId == id);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        [Authorize(Roles = "Host")]
        public IActionResult Create()
        {
            // Populate categories for the dropdown
            ViewBag.AllCategories = _context.Categories
                                             .Select(c => new SelectListItem
                                             {
                                                 Value = c.CategoryId.ToString(),
                                                 Text = c.CategoryName
                                             })
                                             .ToList();

            // Populate states for the dropdown
            ViewBag.AllStates = Enum.GetValues(typeof(StateAbbr))
                                    .Cast<StateAbbr>()
                                    .Select(s => new SelectListItem
                                    {
                                        Value = s.ToString(),
                                        Text = s.ToString()
                                    })
                                    .ToList();

            return View();
        }


        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property property)
        {
            // Check if model state is valid
            if (!ModelState.IsValid)
            {
                // Repopulate the categories dropdown if validation fails
                ViewBag.AllCategories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                    .ToList();

                return View(property);
            }

            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("User not found.");
            }

            // Assign the current user as the Host
            property.Host = currentUser;

            // Look up the Category using the CategoryId
            property.Category = _context.Categories
                .FirstOrDefault(c => c.CategoryId == property.Category.CategoryId);

            if (property.Category == null)
            {
                ModelState.AddModelError("CategoryId", "Invalid category selected.");
                ViewBag.AllCategories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                    .ToList();
                return View(property);
            }

            // Add the property to the context and save changes
            _context.Add(property);
            await _context.SaveChangesAsync();

            // Redirect to the HostDash page upon successful creation
            TempData["Message"] = "Property created successfully.";
            return RedirectToAction("HostDash", "Home");
        } */


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property property)
        {
            Console.WriteLine("Entering Property Create POST method"); // Debugging log

            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                Console.WriteLine("User not found."); // Debugging log
                return Unauthorized("User not found.");
            }

            property.Host = currentUser;

            var categoryId = property.Category?.CategoryId;

            if (categoryId == null)
            {
                Console.WriteLine("Invalid category selected."); // Debugging log
                TempData["Error"] = "Invalid category selected.";
                ViewBag.AllCategories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                    .ToList();
                return View(property);
            }

            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found in database."); // Debugging log
                TempData["Error"] = "Selected category does not exist.";
                ViewBag.AllCategories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                    .ToList();
                return View(property);
            }

            property.Category = category;


            // Validation: Ensure required fields are filled
            if (string.IsNullOrWhiteSpace(property.Street) ||
                string.IsNullOrWhiteSpace(property.City) ||
                string.IsNullOrWhiteSpace(property.State.ToString()) ||
                property.Bedrooms <= 0 ||
                property.Bathrooms <= 0 ||
                property.WeekdayPrice <= 0)
            {
                Console.WriteLine("Validation failed. Required fields are missing or invalid."); // Debugging log
                TempData["Error"] = "Please fill in all required fields correctly.";
                ViewBag.AllCategories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                    .ToList();
                return View(property);
            }

            // Add the property to the context and save changes
            _context.Add(property);
            await _context.SaveChangesAsync();

            Console.WriteLine("Property created successfully."); // Debugging log
            TempData["Message"] = "Property created successfully.";
            return RedirectToAction("HostDash", "Home");
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Properties.Include(p => p.Category).FirstOrDefaultAsync(p => p.PropertyId == id);
            if (property == null)
            {
                return NotFound();
            }

            ViewBag.AllCategories = _context.Categories
                                             .Select(c => new SelectListItem
                                             {
                                                 Value = c.CategoryId.ToString(),
                                                 Text = c.CategoryName
                                             })
                                             .ToList();

            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Street,City,State,ZipCode,Bedrooms,Bathrooms,GuestsAllowed,PetsAllowed,FreeParking,WeekdayPrice,WeekendPrice,CleaningFee,DiscountRate,MinNightsForDiscount,PropertyStatus")] Property updatedProperty, int categoryId)
        {
            // Retrieve the existing property from the database
            var existingProperty = await _context.Properties
                .Include(p => p.Category)
                .Include(p => p.Host)
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (existingProperty == null)
            {
                return NotFound("Property not found.");
            }

            // Ensure that the user is authorized to edit the property
            var currentUser = await _userManager.GetUserAsync(User);
            if (existingProperty.Host?.Id != currentUser.Id)
            {
                return Unauthorized("You are not authorized to edit this property.");
            }

            // Update fields that can be modified
            existingProperty.Street = updatedProperty.Street;
            existingProperty.City = updatedProperty.City;
            existingProperty.State = updatedProperty.State;
            existingProperty.ZipCode = updatedProperty.ZipCode;
            existingProperty.Bedrooms = updatedProperty.Bedrooms;
            existingProperty.Bathrooms = updatedProperty.Bathrooms;
            existingProperty.GuestsAllowed = updatedProperty.GuestsAllowed;
            existingProperty.PetsAllowed = updatedProperty.PetsAllowed;
            existingProperty.FreeParking = updatedProperty.FreeParking;
            existingProperty.WeekdayPrice = updatedProperty.WeekdayPrice;
            existingProperty.WeekendPrice = updatedProperty.WeekendPrice;
            existingProperty.CleaningFee = updatedProperty.CleaningFee;
            existingProperty.DiscountRate = updatedProperty.DiscountRate;
            existingProperty.MinNightsForDiscount = updatedProperty.MinNightsForDiscount;
            existingProperty.PropertyStatus = updatedProperty.PropertyStatus;

            // Update the category
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (category != null)
            {
                existingProperty.Category = category;
            }

            try
            {
                _context.Update(existingProperty);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Property updated successfully.";
                return RedirectToAction("HostDash", "Home");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("An error occurred while updating the property. Please try again.");
            }
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Properties.Include(p => p.Category).FirstOrDefaultAsync(m => m.PropertyId == id);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                _context.Properties.Remove(property);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.PropertyId == id);
        }
        
        //Advanced Searh
        [AllowAnonymous]
        public IActionResult AdvancedSearch()
        {
            // Populate the view model with categories for the dropdown
            var viewModel = new PropertySearchViewModel
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AdvancedSearch(PropertySearchViewModel searchModel)
        {
            // Start with all properties
            var query = _context.Properties.AsQueryable();

            // Filter by City if provided
            if (!string.IsNullOrEmpty(searchModel.City))
            {
                query = query.Where(p => EF.Functions.Like(p.City, $"%{searchModel.City}%"));
            }

            // Filter by State if provided and valid
            if (!string.IsNullOrEmpty(searchModel.State))
            {
                if (Enum.TryParse(typeof(StateAbbr), searchModel.State, true, out var stateEnum))
                {
                    query = query.Where(p => p.State == (StateAbbr)stateEnum);
                }
            }

            // Filter by Guest Rating if provided
            if (searchModel.AverageRating.HasValue)
            {
                if (searchModel.GuestRatingOperator == GuestRatingOperator.GreaterThan)
                {
                    query = query.Where(p =>
                        (decimal)p.Reviews.Average(r => r.Rating) > searchModel.AverageRating.Value);// Include properties with no reviews (0 rating)
                }
                else if (searchModel.GuestRatingOperator == GuestRatingOperator.LessThan)
                {
                    query = query.Where(p =>
                        (decimal)p.Reviews.Average(r => r.Rating) < searchModel.AverageRating.Value ||
                        p.Reviews.Count() == 0); // Include properties with no reviews (0 rating)
                }
            }


            if (searchModel.Guests.HasValue)
            {
                query = query.Where(p => p.GuestsAllowed >= searchModel.Guests.Value);
            }

            
            // Filter by Minimum Daily Price if provided
            if (searchModel.MinDailyPrice.HasValue)
            {
                query = query.Where(p => p.WeekdayPrice >= searchModel.MinDailyPrice.Value);
            }

            // Filter by Maximum Daily Price if provided
            if (searchModel.MaxDailyPrice.HasValue)
            {
                query = query.Where(p => p.WeekdayPrice <= searchModel.MaxDailyPrice.Value);
            }

            // Filter by Category if provided
            if (searchModel.CategoryId.HasValue && searchModel.CategoryId.Value != 0) // Assuming 0 is for "All Categories"
            {
                query = query.Where(p => p.Category.CategoryId == searchModel.CategoryId.Value);
            }

            // Filter by Bedrooms if provided
            if (searchModel.Bedrooms.HasValue)
            {
                query = query.Where(p => p.Bedrooms >= searchModel.Bedrooms.Value);
            }

            // Filter by Bathrooms if provided
            if (searchModel.Bathrooms.HasValue)
            {
                query = query.Where(p => p.Bathrooms >= searchModel.Bathrooms.Value);
            }

            // Filter for Pets Allowed
            if (searchModel.PetsAllowed.HasValue)
            {
                query = query.Where(p => p.PetsAllowed == searchModel.PetsAllowed.Value);
            }

            // Filter for Free Parking
            if (searchModel.FreeParking.HasValue)
            {
                query = query.Where(p => p.FreeParking == searchModel.FreeParking.Value);
            }

            if (searchModel.CheckInDate.HasValue && searchModel.CheckOutDate.HasValue)
            {
                var checkInDate = searchModel.CheckInDate.Value;
                var checkOutDate = searchModel.CheckOutDate.Value;

                query = query.Where(p =>
                    !p.Unavailabilities.Any(u =>
                        (u.Date < checkOutDate && u.Date > checkInDate)
                    ));
            }

            // Execute query with related data
            var properties = await query
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .ToListAsync();

            // Count all properties for display
            var totalProperties = await _context.Properties.CountAsync();

            // Set ViewBag values for display
            ViewBag.SelectedProperties = properties.Count;
            ViewBag.AllProperties = totalProperties;


            // Return results to the view
            return View("Index", properties);
        }
    }
}
