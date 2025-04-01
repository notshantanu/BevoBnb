using Microsoft.AspNetCore.Identity;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MIS333K_FinalProject.Seeding
{
    public static class SeedAdmins
    {
        public static async Task SeedAdminUser(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                Console.WriteLine("Role 'Admin' created.");
            }

            var admins = new[]
            {
                new { FirstName = "Albert", MiddleInitial = "F", LastName = "Taylor", Email = "taylor@bevobnb.com", UserName = "taylor@bevobnb.com", Password = "TRY563", SSN = "645889563", Address = "467 Nueces St.", ZipCode = "75237", PhoneNumber = "2149036025", Birthday = new DateTime(1954, 8, 14), HireStatus = true },
                new { FirstName = "Molly", MiddleInitial = "R", LastName = "Sheffield", Email = "sheffield@bevobnb.com", UserName = "sheffield@bevobnb.com", Password = "longsnores", SSN = "334557278", Address = "3886 Avenue A", ZipCode = "78736", PhoneNumber = "5124749225", Birthday = new DateTime(1986, 8, 27), HireStatus = true },
                new { FirstName = "Jenny", MiddleInitial = "I", LastName = "MacLeod", Email = "macleod@bevobnb.com", UserName = "macleod@bevobnb.com", Password = "kittys", SSN = "886719249", Address = "2504 Far West Blvd.", ZipCode = "78731", PhoneNumber = "5124723769", Birthday = new DateTime(1984, 12, 5), HireStatus = true },
                new { FirstName = "Michelle", MiddleInitial = "N", LastName = "Rhodes", Email = "rhodes@bevobnb.com", UserName = "rhodes@bevobnb.com", Password = "puppies", SSN = "999990909", Address = "4587 Enfield Rd.", ZipCode = "78293", PhoneNumber = "2102520380", Birthday = new DateTime(1972, 7, 2), HireStatus = true },
                new { FirstName = "Evan", MiddleInitial = "Q", LastName = "Stuart", Email = "stuart@bevobnb.com", UserName = "stuart@bevobnb.com", Password = "coolboi", SSN = "212121212", Address = "5576 Toro Ring", ZipCode = "78279", PhoneNumber = "2105415031", Birthday = new DateTime(1984, 4, 17), HireStatus = true },
                new { FirstName = "Ron", MiddleInitial = "P", LastName = "Swanson", Email = "swanson@bevobnb.com", UserName = "swanson@bevobnb.com", Password = "swanbong", SSN = "444444444", Address = "245 River Rd", ZipCode = "78731", PhoneNumber = "5124818542", Birthday = new DateTime(1991, 7, 25), HireStatus = true },
                new { FirstName = "Jabriel", MiddleInitial = "M", LastName = "White", Email = "white@bevobnb.com", UserName = "white@bevobnb.com", Password = "456789", SSN = "666666666", Address = "12 Valley View", ZipCode = "77045", PhoneNumber = "8175025605", Birthday = new DateTime(1986, 3, 17), HireStatus = true },
                new { FirstName = "Washington", MiddleInitial = "X", LastName = "Montgomery", Email = "montgomery@bevobnb.com", UserName = "montgomery@bevobnb.com", Password = "python4", SSN = "676767676", Address = "210 Blanco Dr", ZipCode = "77030", PhoneNumber = "8178817122", Birthday = new DateTime(1961, 5, 4), HireStatus = true },
                new { FirstName = "Lisa", MiddleInitial = "O", LastName = "Walker", Email = "walker@bevobnb.com", UserName = "walker@bevobnb.com", Password = "walkameter", SSN = "323232323", Address = "9 Bison Circle", ZipCode = "75238", PhoneNumber = "2143196301", Birthday = new DateTime(2003, 4, 18), HireStatus = true },
                new { FirstName = "Gregory", MiddleInitial = "J", LastName = "Chang", Email = "chang@bevobnb.com", UserName = "chang@bevobnb.com", Password = "pupgang", SSN = "111222233", Address = "9003 Joshua St", ZipCode = "78260", PhoneNumber = "2103521329", Birthday = new DateTime(1958, 4, 26), HireStatus = true },
                new { FirstName = "Derek", MiddleInitial = "X", LastName = "Dreibrodt", Email = "derek@bevobnb.com", UserName = "derek@bevobnb.com", Password = "2cool4u", SSN = "151515157", Address = "4 Privet Dr", ZipCode = "78705", PhoneNumber = "5125556789", Birthday = new DateTime(2001, 1, 1), HireStatus = true },
                new { FirstName = "Amy", MiddleInitial = "K", LastName = "Rester", Email = "rester@bevobnb.com", UserName = "rester@bevobnb.com", Password = "KIzGreat", SSN = "878787878", Address = "2110 Speedway", ZipCode = "78705", PhoneNumber = "2103521329", Birthday = new DateTime(2000, 1, 1), HireStatus = true }
            };

            foreach (var adminData in admins)
            {
                // Check if the user already exists
                if (!db.Users.Any(u => u.Email == adminData.Email))
                {
                    // Create a new AppUser instance
                    var admin = new AppUser
                    {
                        Email = adminData.Email,
                        UserName = adminData.UserName,
                        LastName = adminData.LastName,
                        FirstName = adminData.FirstName,
                        MiddleInitial = adminData.MiddleInitial,
                        Address = adminData.Address,
                        ZipCode = adminData.ZipCode,
                        PhoneNumber = adminData.PhoneNumber,
                        Birthday = adminData.Birthday,
                        HireStatus = adminData.HireStatus,
                        SSN = adminData.SSN
                    };

                    // Use the plain-text password during user creation
                    var result = await userManager.CreateAsync(admin, adminData.Password);

                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Seeded admin user: {admin.FirstName} {admin.LastName}");

                        // Assign the "Admin" role
                        var roleResult = await userManager.AddToRoleAsync(admin, "Admin");
                        if (roleResult.Succeeded)
                        {
                            Console.WriteLine($"Assigned role 'Admin' to {admin.Email}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to assign role 'Admin' to {admin.Email}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to seed admin user {admin.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Admin user already exists: {adminData.Email}");
                }
            }
        }
    }
}
