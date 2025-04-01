using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


//TODO: Update this using statement to include your project name
using MIS333K_FinalProject.Models;
using MIS333K_FinalProject.DAL;
using System;
using MIS333K_FinalProject.Seeding;

//TODO: Upddate this namespace to match your project name
namespace MIS333K_FinalProject.Controllers
{
    public class SeedController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedController(AppDbContext db, UserManager<AppUser> um, RoleManager<IdentityRole> rm)
        {
            _context = db;
            _userManager = um;
            _roleManager = rm;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SeedRoles()
        {
            try
            {
                //call the method to seed the roles
                await Seeding.SeedRoles.AddAllRoles(_roleManager);
            }
            catch (Exception ex)
            {
                //add the error messages to a list of strings
                List<String> errorList = new List<String>();

                //Add the outer message
                errorList.Add(ex.Message);

                //Add the message from the inner exception
                errorList.Add(ex.InnerException.Message);

                //Add additional inner exception messages, if there are any
                if (ex.InnerException.InnerException != null)
                {
                    errorList.Add(ex.InnerException.InnerException.Message);
                }

                return View("Error", errorList);
            }

            //this is the happy path - seeding worked!
            return View("Confirm");
        }

        public async Task<IActionResult> SeedOneUser()
        {
            try
            {
                // Call the method to seed one user and assign them a role
                await Seeding.SeedUsers.SeedOneCustomer(_userManager, _roleManager, _context);
            }
            catch (Exception ex)
            {
                List<string> errorList = new List<string> { ex.Message };
                if (ex.InnerException != null) errorList.Add(ex.InnerException.Message);
                return View("Error", errorList);
            }

            return View("Confirm");
        }

        public IActionResult SeedAllCustomers()
        {
            try
            {
                // Call the method to seed all customers
                SeedUsers.SeedAllCustomers(_userManager, _roleManager, _context).Wait();
                Console.WriteLine("Seeded all customers successfully!");
            }
            catch (Exception ex)
            {
                // Handle errors
                List<string> errorList = new List<string> { ex.Message };
                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }
                return View("Error", errorList);
            }

            // Success path
            return View("Confirm");
        }



        public async Task<IActionResult> SeedAllAdmins()
        {
            try
            {

                await SeedAdmins.SeedAdminUser(_userManager, _roleManager, _context);
                Console.WriteLine("Seeded all admins successfully!");
            }
            catch (Exception ex)
            {

                List<string> errorList = new List<string> { ex.Message };
                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }
                return View("Error", errorList);
            }
            return View("Confirm");
        }

        public async Task<IActionResult> SeedAllHosts()
        {
            try
            {

                await SeedHosts.SeedAllHosts(_userManager, _roleManager, _context);
                Console.WriteLine("Seeded all hosts successfully!");
            }
            catch (Exception ex)
            {

                List<string> errorList = new List<string> { ex.Message };
                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }
                return View("Error", errorList);
            }
            return View("Confirm");
        }

        public IActionResult SeedAllCategories()
        {
            try
            {
                // Call the method to seed all categories
                SeedData.SeedAllCategories(_context);
            }
            catch (Exception ex)
            {
                // Handle any errors and display them on an error page
                List<string> errorList = new List<string>
                    {
                        // Add the outer exception message
                        ex.Message
                    };

                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);

                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }

                return View("Error", errorList);
            }

            // If successful, return a confirmation view
            return View("Confirm");
        }


        public IActionResult SeedAllProperties()
        {
            try
            {
                // Call the method to seed all categories
                SeedProperties.SeedAllProperties(_context);
            }
            catch (Exception ex)
            {
                // Handle any errors and display them on an error page
                List<string> errorList = new List<string>
                    {
                        // Add the outer exception message
                        ex.Message
                    };

                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);

                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }

                return View("Error", errorList);
            }

            // If successful, return a confirmation view
            return View("Confirm");
        }

        public IActionResult SeedAllUnavailabilities()
        {
            try
            {
                // Call the method to seed all categories
                SeedUnavailability.SeedAllUnavailabilities(_context);
            }
            catch (Exception ex)
            {
                // Handle any errors and display them on an error page
                List<string> errorList = new List<string>
                    {
                        // Add the outer exception message
                        ex.Message
                    };

                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);

                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }

                return View("Error", errorList);
            }

            // If successful, return a confirmation view
            return View("Confirm");
        }

        public IActionResult SeedOneProperty()
        {
            try
            {
                SeedProperties.SeedOneProperty(_context);
                return View("Confirm");
            }
            catch (Exception ex)
            {
                List<string> errorList = new List<string> { ex.Message };
                if (ex.InnerException != null) errorList.Add(ex.InnerException.Message);
                return View("Error", errorList);
            }
        }

        public IActionResult SeedOneReview()
        {
            try
            {
                SeedReviews.SeedOneReview(_context);
                return View("Confirm");
            }
            catch (Exception ex)
            {
                List<string> errorList = new List<string> { ex.Message };
                if (ex.InnerException != null) errorList.Add(ex.InnerException.Message);
                return View("Error", errorList);
            }
        }

        public IActionResult SeedAllReviews()
        {
            try
            {
                // Call the method to seed all categories
                SeedReviews.SeedAllReviews(_context);
            }
            catch (Exception ex)
            {
                // Handle any errors and display them on an error page
                List<string> errorList = new List<string>
                    {
                        // Add the outer exception message
                        ex.Message
                    };

                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);

                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }

                return View("Error", errorList);
            }

            // If successful, return a confirmation view
            return View("Confirm");
        }

        public IActionResult SeedOneReservation()
        {
            try
            {
                SeedReservations.SeedOneReservation(_context);
                return View("Confirm");
            }
            catch (Exception ex)
            {
                List<string> errorList = new List<string> { ex.Message };
                if (ex.InnerException != null) errorList.Add(ex.InnerException.Message);
                return View("Error", errorList);
            }
        }
        public IActionResult SeedAllReservations()
        {
            try
            {
                // Call the method to seed all categories
                SeedReservations.SeedAllReservations(_context);
            }
            catch (Exception ex)
            {
                // Handle any errors and display them on an error page
                List<string> errorList = new List<string>
                    {
                        // Add the outer exception message
                        ex.Message
                    };

                if (ex.InnerException != null)
                {
                    errorList.Add(ex.InnerException.Message);

                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }
                }

                return View("Error", errorList);
            }

            // If successful, return a confirmation view
            return View("Confirm");
        }
    }
}

