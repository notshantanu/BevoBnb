using Microsoft.AspNetCore.Identity;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MIS333K_FinalProject.Seeding
{
    public static class SeedHosts
    {
        public static async Task SeedAllHosts(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            // Ensure the "Host" role exists
            if (!await roleManager.RoleExistsAsync("Host"))
            {
                await roleManager.CreateAsync(new IdentityRole("Host"));
                Console.WriteLine("Role 'Host' created.");
            }

            // Host data with plain-text passwords
            var hosts = new[]
            {
                new { Email = "jacobs@yahoo.com", UserName = "jacobs@yahoo.com", Password = "tj2245", LastName = "Jacobs", FirstName = "Todd", MiddleInitial = "L", Address = "4564 Elm St.", ZipCode = "77003", PhoneNumber = "8176663948", Birthday = new DateTime(1978, 1, 29), HireStatus = false },
                new { Email = "rice@yahoo.com", UserName = "rice@yahoo.com", Password = "ricearoni", LastName = "Rice", FirstName = "Eryn", MiddleInitial = "M", Address = "3405 Rio Grande", ZipCode = "75261", PhoneNumber = "2148545987", Birthday = new DateTime(2003, 6, 11), HireStatus = false },
                new { Email = "ingram@gmail.com", UserName = "ingram@gmail.com", Password = "ingram68", LastName = "Ingram", FirstName = "John", MiddleInitial = "R", Address = "6548 La Posada Ct.", ZipCode = "78705", PhoneNumber = "5127049017", Birthday = new DateTime(1980, 6, 25), HireStatus = false },
                new { Email = "martinez@aol.com", UserName = "martinez@aol.com", Password = "party1", LastName = "Martinez", FirstName = "Paul", MiddleInitial = "G", Address = "8295 Sunset Blvd.", ZipCode = "78239", PhoneNumber = "2105859369", Birthday = new DateTime(1969, 6, 25), HireStatus = false },
                new { Email = "tanner@utexas.edu", UserName = "tanner@utexas.edu", Password = "sandman", LastName = "Tanner", FirstName = "Jared", MiddleInitial = "F", Address = "4347 Almstead", ZipCode = "78761", PhoneNumber = "5129527803", Birthday = new DateTime(1979, 6, 2), HireStatus = false },
                new { Email = "chung@yahoo.com", UserName = "chung@yahoo.com", Password = "listen", LastName = "Chung", FirstName = "Lauren", MiddleInitial = "R", Address = "234 RR 12", ZipCode = "78268", PhoneNumber = "2107053952", Birthday = new DateTime(1976, 3, 24), HireStatus = false },
                new { Email = "loter@yahoo.com", UserName = "loter@yahoo.com", Password = "pottery", LastName = "Loter", FirstName = "Wandavison", MiddleInitial = "T", Address = "3453 RR 3235", ZipCode = "78732", PhoneNumber = "5124650249", Birthday = new DateTime(1966, 9, 23), HireStatus = false },
                new { Email = "morales@aol.com", UserName = "morales@aol.com", Password = "heckin", LastName = "Morales", FirstName = "Heather", MiddleInitial = "R", Address = "4501 RR 140", ZipCode = "77031", PhoneNumber = "8177529019", Birthday = new DateTime(1971, 1, 16), HireStatus = false },
                new { Email = "rankin@yahoo.com", UserName = "rankin@yahoo.com", Password = "rankmark", LastName = "Rankin", FirstName = "Martin", MiddleInitial = "P", Address = "340 Second St", ZipCode = "78703", PhoneNumber = "5122997370", Birthday = new DateTime(1961, 5, 16), HireStatus = false },
                new { Email = "gonzalez@aol.com", UserName = "gonzalez@aol.com", Password = "gg2017", LastName = "Gonzalez", FirstName = "Garth", MiddleInitial = "R", Address = "103 Manor Rd", ZipCode = "75260", PhoneNumber = "2142415970", Birthday = new DateTime(1993, 12, 10), HireStatus = false }
            };

            foreach (var hostData in hosts)
            {
                // Check if the user already exists
                if (!db.Users.Any(u => u.Email == hostData.Email))
                {
                    // Create a new AppUser instance
                    var host = new AppUser
                    {
                        Email = hostData.Email,
                        UserName = hostData.UserName,
                        LastName = hostData.LastName,
                        FirstName = hostData.FirstName,
                        MiddleInitial = hostData.MiddleInitial,
                        Address = hostData.Address,
                        ZipCode = hostData.ZipCode,
                        PhoneNumber = hostData.PhoneNumber,
                        Birthday = hostData.Birthday,
                        HireStatus = hostData.HireStatus
                    };

                    // Use the plain-text password during user creation
                    var result = await userManager.CreateAsync(host, hostData.Password);
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Seeded host user: {host.FirstName} {host.LastName}");

                        // Assign the "Host" role
                        var roleResult = await userManager.AddToRoleAsync(host, "Host");
                        if (roleResult.Succeeded)
                        {
                            Console.WriteLine($"Assigned role 'Host' to {host.Email}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to assign role 'Host' to {host.Email}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to seed host user {host.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Host user already exists: {hostData.Email}");
                }
            }
        }
    }
}
