using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;

namespace MIS333K_FinalProject.Seeding
{
    public class SeedReviews
    {
        public static void SeedOneReview(AppDbContext db)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Reviews ON");

                    // Fetch the Property
                    var property = db.Properties.FirstOrDefault(p => p.PropertyId == 3183);
                    if (property == null)
                    {
                        throw new Exception("Property with PropertyId '3183' not found in the database.");
                    }

                    // Fetch the Customer
                    var customer = db.Users.FirstOrDefault(u => u.Email == "tanner@utexas.edu");
                    if (customer == null)
                    {
                        throw new Exception("Customer with email 'tanner@utexas.edu' not found in the database.");
                    }

                    // Add the review
                    db.Reviews.Add(new Models.Review
                    {
                        //ReviewID = 4001,
                        Rating = 5,
                        ReviewText = "Amazing property, had a wonderful stay!",
                        HostComments = "Thank you for your review!",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = property.PropertyId,
                        CustomerEmail = customer.Email,
                        Property = property,
                        Customer = customer
                    });

                    db.SaveChanges();
                    //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Reviews OFF");
                    transaction.Commit();

                    Console.WriteLine("Successfully seeded review with ReviewID = 4001.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error seeding review: " + ex.Message);
                    throw;
                }
            }
        }
        public static void SeedAllReviews(AppDbContext db)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Reviews ON");

                    // List of reviews to add
                    var reviews = new List<Models.Review>
                {
                    new Models.Review
                    {
                        //ReviewID = 4001,
                        Rating = 4,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3127)?.PropertyId ?? throw new Exception("PropertyId 3127 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com")?.Email ?? throw new Exception("Customer father.Ingram@aool.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3127),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4002,
                        Rating = 3,
                        ReviewText = "It was meh, ya know? It was really close to the coast, but the beaches were kinda trashed. The apartment was nice, but there wasn't an elevator.",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3085)?.PropertyId ?? throw new Exception("PropertyId 3085 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")?.Email ?? throw new Exception("Customer orielly@foxnets.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3085),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4003,
                        Rating = 4,
                        HostComments = "The customer did not provide a valid reason for this rating.",
                        DisputeStatus = DisStatus.Disputed,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3161)?.PropertyId ?? throw new Exception("PropertyId 3161 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com")?.Email ?? throw new Exception("Customer father.Ingram@aool.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3161),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4004,
                        Rating = 2,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3127)?.PropertyId ?? throw new Exception("PropertyId 3127 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com")?.Email ?? throw new Exception("Customer tuck33@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3127),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4005,
                        Rating = 3,
                        ReviewText = "Nebraska was... interesting",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3107)?.PropertyId ?? throw new Exception("PropertyId 3107 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com")?.Email ?? throw new Exception("Customer father.Ingram@aool.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4006,
                        Rating = 1,
                        ReviewText = "There was corn EVERYWHERE! I looked left and BAM, CORN. Looked right, BAM, CORN",
                        HostComments = "It is not my fault there was corn. It was not my corn!",
                        DisputeStatus = DisStatus.Disputed,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3107)?.PropertyId ?? throw new Exception("PropertyId 3107 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com")?.Email ?? throw new Exception("Customer tfreeley@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com")
                    },

                    new Models.Review
                    {
                        //ReviewID = 4007,
                        Rating = 1,
                        ReviewText = "Worst. Stay. Ever. Never using BevoBnB again",
                        HostComments = "BevoBnB is the best",
                        DisputeStatus = DisStatus.ValidDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3107)?.PropertyId ?? throw new Exception("PropertyId 3107 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "ra@aoo.com")?.Email ?? throw new Exception("Customer ra@aoo.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "ra@aoo.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4008,
                        Rating = 5,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3065)?.PropertyId ?? throw new Exception("PropertyId 3065 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")?.Email ?? throw new Exception("Customer orielly@foxnets.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4009,
                        Rating = 2,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3133)?.PropertyId ?? throw new Exception("PropertyId 3133 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")?.Email ?? throw new Exception("Customer orielly@foxnets.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3133),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4010,
                        Rating = 1,
                        ReviewText = "It was SO hard to book this place. Who coded this site anyway? ;)",
                        HostComments = "The website was coded by students so the owner should not be penalized!",
                        DisputeStatus = DisStatus.InvalidDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3065)?.PropertyId ?? throw new Exception("PropertyId 3065 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com")?.Email ?? throw new Exception("Customer tfreeley@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4011,
                        Rating = 4,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3067)?.PropertyId ?? throw new Exception("PropertyId 3067 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com")?.Email ?? throw new Exception("Customer tuck33@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3067),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com")
                    },

                    new Models.Review
                    {
                        //ReviewID = 4012,
                        Rating = 5,
                        ReviewText = "This place rocked!",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3065)?.PropertyId ?? throw new Exception("PropertyId 3065 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "ra@aoo.com")?.Email ?? throw new Exception("Customer ra@aoo.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "ra@aoo.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4013,
                        Rating = 4,
                        HostComments = "I do not understand this.",
                        DisputeStatus = DisStatus.ValidDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3065)?.PropertyId ?? throw new Exception("PropertyId 3065 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com")?.Email ?? throw new Exception("Customer fd@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4014,
                        Rating = 4,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3133)?.PropertyId ?? throw new Exception("PropertyId 3133 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "lamemartin.Martin@aool.com")?.Email ?? throw new Exception("Customer lamemartin.Martin@aool.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3133),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "lamemartin.Martin@aool.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4015,
                        Rating = 1,
                        ReviewText = "There were 1...5...22 roaches? I lost count.",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3107)?.PropertyId ?? throw new Exception("PropertyId 3107 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com")?.Email ?? throw new Exception("Customer fd@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4016,
                        Rating = 1,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3085)?.PropertyId ?? throw new Exception("PropertyId 3085 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com")?.Email ?? throw new Exception("Customer sheff44@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3085),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4017,
                        Rating = 4,
                        ReviewText = "I LOVED the place! Had a nice view of the mountains",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3087)?.PropertyId ?? throw new Exception("PropertyId 3087 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com")?.Email ?? throw new Exception("Customer fd@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3087),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4018,
                        Rating = 5,
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3087)?.PropertyId ?? throw new Exception("PropertyId 3087 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com")?.Email ?? throw new Exception("Customer tuck33@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3087),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4019,
                        Rating = 5,
                        ReviewText = "My stay was amazing! Saved my marriage",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3127)?.PropertyId ?? throw new Exception("PropertyId 3127 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")?.Email ?? throw new Exception("Customer orielly@foxnets.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3127),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4020,
                        Rating = 2,
                        HostComments = "Why??",
                        DisputeStatus = DisStatus.InvalidDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3133)?.PropertyId ?? throw new Exception("PropertyId 3133 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com")?.Email ?? throw new Exception("Customer sheff44@puppy.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3133),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com")
                    },
                    new Models.Review
                    {
                        //ReviewID = 4021,
                        Rating = 2,
                        ReviewText = "My wife's attitude was the only thing rougher than the sand at the nearby beaches",
                        DisputeStatus = DisStatus.NoDispute,
                        PropertyNumber = db.Properties.FirstOrDefault(p => p.PropertyId == 3161)?.PropertyId ?? throw new Exception("PropertyId 3161 not found."),
                        CustomerEmail = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")?.Email ?? throw new Exception("Customer orielly@foxnets.com not found."),
                        Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3161),
                        Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com")
                    }


                };


                    // Add reviews to the database
                    db.Reviews.AddRange(reviews);

                    db.SaveChanges();
                    //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Reviews OFF");
                    transaction.Commit();

                    Console.WriteLine("Successfully seeded all reviews.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error seeding reviews: " + ex.Message);
                    throw;
                }
            }
        }



    }

}
