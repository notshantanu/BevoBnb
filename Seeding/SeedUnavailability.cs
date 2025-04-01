using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;

namespace MIS333K_FinalProject.Seeding
{
    public static class SeedUnavailability
    {
        public static void SeedAllUnavailabilities(AppDbContext db)
        {
            // Retrieving properties from the database to associate them with unavailabilities
            var properties = db.Properties.ToList();

            
            List<Unavailability> unavailabilities = new List<Unavailability>
            {
                new Unavailability { PropertyId = 3009, Date = new DateTime(2024, 12, 04), Property = properties.FirstOrDefault(p => p.PropertyId == 3009) },
                new Unavailability { PropertyId = 3009, Date = new DateTime(2024, 12, 05), Property = properties.FirstOrDefault(p => p.PropertyId == 3009) },
                new Unavailability { PropertyId = 3172, Date = new DateTime(2024, 12, 30), Property = properties.FirstOrDefault(p => p.PropertyId == 3172) },
                new Unavailability { PropertyId = 3172, Date = new DateTime(2024, 12, 31), Property = properties.FirstOrDefault(p => p.PropertyId == 3172) },
                new Unavailability { PropertyId = 3172, Date = new DateTime(2025, 01, 01), Property = properties.FirstOrDefault(p => p.PropertyId == 3172) },
                new Unavailability { PropertyId = 3113, Date = new DateTime(2024, 12, 05), Property = properties.FirstOrDefault(p => p.PropertyId == 3113) },
                new Unavailability { PropertyId = 3113, Date = new DateTime(2024, 12, 06), Property = properties.FirstOrDefault(p => p.PropertyId == 3113) },
                new Unavailability { PropertyId = 3113, Date = new DateTime(2024, 12, 07), Property = properties.FirstOrDefault(p => p.PropertyId == 3113) },
                new Unavailability { PropertyId = 3099, Date = new DateTime(2024, 12, 29), Property = properties.FirstOrDefault(p => p.PropertyId == 3099) },
                new Unavailability { PropertyId = 3099, Date = new DateTime(2024, 12, 30), Property = properties.FirstOrDefault(p => p.PropertyId == 3099) },
                new Unavailability { PropertyId = 3099, Date = new DateTime(2024, 12, 31), Property = properties.FirstOrDefault(p => p.PropertyId == 3099) },
                new Unavailability { PropertyId = 3099, Date = new DateTime(2025, 01, 01), Property = properties.FirstOrDefault(p => p.PropertyId == 3099) },
                new Unavailability { PropertyId = 3100, Date = new DateTime(2024, 12, 31), Property = properties.FirstOrDefault(p => p.PropertyId == 3100) }
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Fetch the Host
                    foreach (Unavailability unavailability in unavailabilities)
                    {
                        if (!db.Unavailabilities.Any(u => u.UnavailabilityId == unavailability.UnavailabilityId &&
                                                          u.Date == unavailability.Date))
                        {
                            db.Unavailabilities.Add(unavailability);
                        }
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    Console.WriteLine("Successfully seeded unavailability");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error seeding unavailability: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
