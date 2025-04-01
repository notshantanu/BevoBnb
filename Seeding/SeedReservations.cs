using System;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;

namespace MIS333K_FinalProject.Seeding
{
    public class SeedReservations
    {
        public static void SeedOneReservation(AppDbContext db)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Fetch the Customer
                    var customer = db.Users.FirstOrDefault(g => g.Email == "tanner@puppy.com");
                    if (customer == null)
                    {
                        throw new Exception("Customer with email 'tanner@puppy.com' not found in the database.");
                    }

                    // Fetch the Property (You can also adjust the Property lookup based on your needs)
                    var property = db.Properties.FirstOrDefault(g => g.PropertyId == 3127);
                    if (property == null)
                    {
                        throw new Exception("Property with PropertyId '3127' not found in the database.");
                    }

                    //var confirmationNumber = db.Reservations.Any()
                        //? db.Reservations.Max(r => r.ConfirmationNumber) + 1
                       // : 21901;

                    // Add the Reservation
                    db.Reservations.Add(new Models.Reservation
                    {
                        StartDate = new DateTime(2024, 11, 1), // Set your start date here
                        EndDate = new DateTime(2024, 11, 3), // Set your end date here
                        WeekendPrice = 249.39m, // Set weekend price
                        WeekdayPrice = 134.72m, // Set weekday price
                        CleaningFee = 19.19m, // Set cleaning fee
                        DiscountRate = 0.0m, // Set discount rate (0 for no discount)
                        NumOfGuests = 1, // Set number of guests
                        ConfirmationNumber = 21901, // Set a unique confirmation number
                        ReservationStatus = ResStatus.Valid, // You can adjust this status
                        Property = property,
                        Customer = customer
                    });

                    db.SaveChanges();
                    transaction.Commit();

                    Console.WriteLine("Successfully seeded reservation for PropertyId = 3127 with ConfirmationNumber = 21900.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error seeding reservation: " + ex.Message);
                    throw;
                }
            }
        }




        public static void SeedAllReservations(AppDbContext db)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var reservations = new List<Models.Reservation>
                {
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 1),
                            EndDate = new DateTime(2024, 11, 3),
                            WeekendPrice = 249.39m,
                            WeekdayPrice = 134.72m,
                            CleaningFee = 19.19m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21900,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3133) ?? throw new Exception("PropertyId 3133 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com") ?? throw new Exception("Customer sheff44@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 4),
                            EndDate = new DateTime(2024, 11, 6),
                            WeekendPrice = 207.51m,
                            WeekdayPrice = 204.67m,
                            CleaningFee = 26.36m,
                            DiscountRate = 0.22m,
                            NumOfGuests = 2,
                            ConfirmationNumber = 21901,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3150) ?? throw new Exception("PropertyId 3150 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "luce_chuck@puppy.com") ?? throw new Exception("Customer luce_chuck@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 6),
                            EndDate = new DateTime(2024, 11, 10),
                            WeekendPrice = 262.77m,
                            WeekdayPrice = 163.30m,
                            CleaningFee = 13.74m,
                            DiscountRate = 0.16m,
                            NumOfGuests = 14,
                            ConfirmationNumber = 21901,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3116) ?? throw new Exception("PropertyId 3116 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "luce_chuck@puppy.com") ?? throw new Exception("Customer luce_chuck@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 7),
                            EndDate = new DateTime(2024, 11, 12),
                            WeekendPrice = 249.39m,
                            WeekdayPrice = 134.72m,
                            CleaningFee = 19.19m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21902,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3133) ?? throw new Exception("PropertyId 3133 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "lamemartin.Martin@aool.com") ?? throw new Exception("Customer lamemartin.Martin@aool.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 6),
                            EndDate = new DateTime(2024, 11, 18),
                            WeekendPrice = 286.53m,
                            WeekdayPrice = 163.68m,
                            CleaningFee = 25.57m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 9,
                            ConfirmationNumber = 21903,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3067) ?? throw new Exception("PropertyId 3067 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com") ?? throw new Exception("Customer tuck33@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 7),
                            EndDate = new DateTime(2024, 11, 15),
                            WeekendPrice = 209.77m,
                            WeekdayPrice = 147.55m,
                            CleaningFee = 27.65m,
                            DiscountRate = 0.09m,
                            NumOfGuests = 3,
                            ConfirmationNumber = 21904,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3075) ?? throw new Exception("PropertyId 3075 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "liz@puppy.com") ?? throw new Exception("Customer liz@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 17),
                            EndDate = new DateTime(2024, 11, 22),
                            WeekendPrice = 180.86m,
                            WeekdayPrice = 224.98m,
                            CleaningFee = 11.91m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 12,
                            ConfirmationNumber = 21905,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3127) ?? throw new Exception("PropertyId 3127 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com") ?? throw new Exception("Customer orielly@foxnets.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 22),
                            EndDate = new DateTime(2024, 11, 27),
                            WeekendPrice = 249.39m,
                            WeekdayPrice = 134.72m,
                            CleaningFee = 19.19m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21906,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3133) ?? throw new Exception("PropertyId 3133 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com") ?? throw new Exception("Customer orielly@foxnets.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 23),
                            EndDate = new DateTime(2024, 12, 1),
                            WeekendPrice = 271.49m,
                            WeekdayPrice = 93.35m,
                            CleaningFee = 8.54m,
                            DiscountRate = 0.18m,
                            NumOfGuests = 5,
                            ConfirmationNumber = 21907,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3082) ?? throw new Exception("PropertyId 3082 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "elowe@netscrape.net") ?? throw new Exception("Customer elowe@netscrape.net not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 24),
                            EndDate = new DateTime(2024, 12, 4),
                            WeekendPrice = 152.09m,
                            WeekdayPrice = 174.87m,
                            CleaningFee = 18.44m,
                            DiscountRate = 0.08m,
                            NumOfGuests = 11,
                            ConfirmationNumber = 21908,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3172) ?? throw new Exception("PropertyId 3172 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "victoria@puppy.com") ?? throw new Exception("Customer victoria@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 29),
                            EndDate = new DateTime(2024, 12, 18),
                            WeekendPrice = 167.53m,
                            WeekdayPrice = 117.45m,
                            CleaningFee = 24.75m,
                            DiscountRate = 0.17m,
                            NumOfGuests = 10,
                            ConfirmationNumber = 21909,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3056) ?? throw new Exception("PropertyId 3056 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "wjhearniii@umch.edu") ?? throw new Exception("Customer wjhearniii@umch.edu not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 28),
                            EndDate = new DateTime(2024, 12, 1),
                            WeekendPrice = 139.83m,
                            WeekdayPrice = 155.03m,
                            CleaningFee = 21.05m,
                            DiscountRate = 0.09m,
                            NumOfGuests = 13,
                            ConfirmationNumber = 21910,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065) ?? throw new Exception("PropertyId 3065 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com") ?? throw new Exception("Customer fd@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 1),
                            EndDate = new DateTime(2024, 11, 5),
                            WeekendPrice = 286.53m,
                            WeekdayPrice = 163.68m,
                            CleaningFee = 25.57m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 9,
                            ConfirmationNumber = 21911,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3067) ?? throw new Exception("PropertyId 3067 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com") ?? throw new Exception("Customer tfreeley@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 15),
                            EndDate = new DateTime(2024, 12, 1),
                            WeekendPrice = 117.17m,
                            WeekdayPrice = 215.38m,
                            CleaningFee = 24.31m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 14,
                            ConfirmationNumber = 21912,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3094) ?? throw new Exception("PropertyId 3094 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "wendy@puppy.com") ?? throw new Exception("Customer wendy@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 5),
                            EndDate = new DateTime(2024, 12, 3),
                            WeekendPrice = 109.87m,
                            WeekdayPrice = 150.69m,
                            CleaningFee = 13.30m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 7,
                            ConfirmationNumber = 21913,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3148) ?? throw new Exception("PropertyId 3148 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "444444.Dave@aool.com") ?? throw new Exception("Customer 444444.Dave@aool.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 1),
                            EndDate = new DateTime(2024, 11, 16),
                            WeekendPrice = 180.86m,
                            WeekdayPrice = 224.98m,
                            CleaningFee = 11.91m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 12,
                            ConfirmationNumber = 21914,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3127) ?? throw new Exception("PropertyId 3127 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com") ?? throw new Exception("Customer father.Ingram@aool.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 8),
                            EndDate = new DateTime(2024, 12, 10),
                            WeekendPrice = 278.17m,
                            WeekdayPrice = 194.84m,
                            CleaningFee = 5.88m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 11,
                            ConfirmationNumber = 21915,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3085) ?? throw new Exception("PropertyId 3085 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com") ?? throw new Exception("Customer orielly@foxnets.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 9),
                            EndDate = new DateTime(2024, 12, 11),
                            WeekendPrice = 228.81m,
                            WeekdayPrice = 140.93m,
                            CleaningFee = 29.74m,
                            DiscountRate = 0.15m,
                            NumOfGuests = 3,
                            ConfirmationNumber = 21916,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3180) ?? throw new Exception("PropertyId 3180 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "hicks43@puppy.com") ?? throw new Exception("Customer hicks43@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 2),
                            EndDate = new DateTime(2024, 12, 5),
                            WeekendPrice = 269.63m,
                            WeekdayPrice = 126.25m,
                            CleaningFee = 8.27m,
                            DiscountRate = 0.16m,
                            NumOfGuests = 10,
                            ConfirmationNumber = 21917,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3161) ?? throw new Exception("PropertyId 3161 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com") ?? throw new Exception("Customer orielly@foxnets.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 1),
                            EndDate = new DateTime(2024, 12, 4),
                            WeekendPrice = 278.17m,
                            WeekdayPrice = 194.84m,
                            CleaningFee = 5.88m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 11,
                            ConfirmationNumber = 21918,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3085) ?? throw new Exception("PropertyId 3085 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com") ?? throw new Exception("Customer sheff44@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 8),
                            EndDate = new DateTime(2024, 12, 9),
                            WeekendPrice = 165.32m,
                            WeekdayPrice = 112.62m,
                            CleaningFee = 24.26m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 12,
                            ConfirmationNumber = 21919,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3021) ?? throw new Exception("PropertyId 3021 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com") ?? throw new Exception("Customer sheff44@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 10),
                            EndDate = new DateTime(2024, 12, 11),
                            WeekendPrice = 108.24m,
                            WeekdayPrice = 205.01m,
                            CleaningFee = 6.56m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 10,
                            ConfirmationNumber = 21919,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3174) ?? throw new Exception("PropertyId 3174 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com") ?? throw new Exception("Customer sheff44@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 9),
                            EndDate = new DateTime(2024, 12, 10),
                            WeekendPrice = 100.37m,
                            WeekdayPrice = 170.25m,
                            CleaningFee = 18.64m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 12,
                            ConfirmationNumber = 21919,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3005) ?? throw new Exception("PropertyId 3005 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "sheff44@puppy.com") ?? throw new Exception("Customer sheff44@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 22),
                            EndDate = new DateTime(2024, 12, 1),
                            WeekendPrice = 269.63m,
                            WeekdayPrice = 126.25m,
                            CleaningFee = 8.27m,
                            DiscountRate = 0.16m,
                            NumOfGuests = 10,
                            ConfirmationNumber = 21919,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3161) ?? throw new Exception("PropertyId 3161 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com") ?? throw new Exception("Customer father.Ingram@aool.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 8),
                            EndDate = new DateTime(2024, 12, 12),
                            WeekendPrice = 182.77m,
                            WeekdayPrice = 127.97m,
                            CleaningFee = 13.02m,
                            DiscountRate = 0.17m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21920,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3087) ?? throw new Exception("PropertyId 3087 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com") ?? throw new Exception("Customer fd@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 8),
                            EndDate = new DateTime(2024, 12, 12),
                            WeekendPrice = 128.05m,
                            WeekdayPrice = 83.34m,
                            CleaningFee = 11.29m,
                            DiscountRate = 0.21m,
                            NumOfGuests = 8,
                            ConfirmationNumber = 21921,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107) ?? throw new Exception("PropertyId 3107 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com") ?? throw new Exception("Customer father.Ingram@aool.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 12),
                            EndDate = new DateTime(2024, 12, 15),
                            WeekendPrice = 204.28m,
                            WeekdayPrice = 204.04m,
                            CleaningFee = 11.07m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 10,
                            ConfirmationNumber = 21921,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3051) ?? throw new Exception("PropertyId 3051 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "father.Ingram@aool.com") ?? throw new Exception("Customer father.Ingram@aool.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 7),
                            EndDate = new DateTime(2024, 12, 31),
                            WeekendPrice = 196.09m,
                            WeekdayPrice = 130.47m,
                            CleaningFee = 14.53m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 3,
                            ConfirmationNumber = 21923,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3073) ?? throw new Exception("PropertyId 3073 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "jeff@puppy.com") ?? throw new Exception("Customer jeff@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 11),
                            EndDate = new DateTime(2024, 12, 24),
                            WeekendPrice = 176.37m,
                            WeekdayPrice = 170.07m,
                            CleaningFee = 8.54m,
                            DiscountRate = 0.09m,
                            NumOfGuests = 13,
                            ConfirmationNumber = 21923,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3097) ?? throw new Exception("PropertyId 3097 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "cmiller@mapster.com") ?? throw new Exception("Customer cmiller@mapster.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 22),
                            EndDate = new DateTime(2024, 11, 29),
                            WeekendPrice = 182.77m,
                            WeekdayPrice = 127.97m,
                            CleaningFee = 13.02m,
                            DiscountRate = 0.17m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21924,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3087) ?? throw new Exception("PropertyId 3087 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com") ?? throw new Exception("Customer tuck33@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 16),
                            EndDate = new DateTime(2024, 12, 22),
                            WeekendPrice = 158.42m,
                            WeekdayPrice = 104.05m,
                            CleaningFee = 5.36m,
                            DiscountRate = 0.23m,
                            NumOfGuests = 5,
                            ConfirmationNumber = 21925,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3038) ?? throw new Exception("PropertyId 3038 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com") ?? throw new Exception("Customer tfreeley@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 20),
                            EndDate = new DateTime(2024, 12, 1),
                            WeekendPrice = 128.05m,
                            WeekdayPrice = 83.34m,
                            CleaningFee = 11.29m,
                            DiscountRate = 0.21m,
                            NumOfGuests = 8,
                            ConfirmationNumber = 21925,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107) ?? throw new Exception("PropertyId 3107 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com") ?? throw new Exception("Customer tfreeley@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 22),
                            EndDate = new DateTime(2024, 12, 28),
                            WeekendPrice = 192.46m,
                            WeekdayPrice = 106.30m,
                            CleaningFee = 17.59m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 4,
                            ConfirmationNumber = 21925,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3113) ?? throw new Exception("PropertyId 3113 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com") ?? throw new Exception("Customer tfreeley@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 28),
                            EndDate = new DateTime(2024, 12, 31),
                            WeekendPrice = 241.45m,
                            WeekdayPrice = 199.68m,
                            CleaningFee = 25.94m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 6,
                            ConfirmationNumber = 21925,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3142) ?? throw new Exception("PropertyId 3142 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com") ?? throw new Exception("Customer tfreeley@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 14),
                            EndDate = new DateTime(2024, 12, 16),
                            WeekendPrice = 123.04m,
                            WeekdayPrice = 89.88m,
                            CleaningFee = 19.14m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21925,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3071) ?? throw new Exception("PropertyId 3071 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com") ?? throw new Exception("Customer tfreeley@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 2),
                            EndDate = new DateTime(2024, 12, 6),
                            WeekendPrice = 139.83m,
                            WeekdayPrice = 155.03m,
                            CleaningFee = 21.05m,
                            DiscountRate = 0.09m,
                            NumOfGuests = 13,
                            ConfirmationNumber = 21926,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065) ?? throw new Exception("PropertyId 3065 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tfreeley@puppy.com") ?? throw new Exception("Customer tfreeley@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 15),
                            EndDate = new DateTime(2024, 12, 24),
                            WeekendPrice = 269.55m,
                            WeekdayPrice = 223.27m,
                            CleaningFee = 11.35m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 9,
                            ConfirmationNumber = 21927,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3157) ?? throw new Exception("PropertyId 3157 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "peterstump@hootmail.com") ?? throw new Exception("Customer peterstump@hootmail.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 1),
                            EndDate = new DateTime(2024, 12, 4),
                            WeekendPrice = 128.05m,
                            WeekdayPrice = 83.34m,
                            CleaningFee = 11.29m,
                            DiscountRate = 0.21m,
                            NumOfGuests = 8,
                            ConfirmationNumber = 21928,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107) ?? throw new Exception("PropertyId 3107 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "ra@aoo.com") ?? throw new Exception("Customer ra@aoo.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 1),
                            EndDate = new DateTime(2024, 12, 2),
                            WeekendPrice = 139.83m,
                            WeekdayPrice = 155.03m,
                            CleaningFee = 21.05m,
                            DiscountRate = 0.09m,
                            NumOfGuests = 13,
                            ConfirmationNumber = 21929,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065) ?? throw new Exception("PropertyId 3065 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com") ?? throw new Exception("Customer orielly@foxnets.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 28),
                            EndDate = new DateTime(2025, 1, 3),
                            WeekendPrice = 229.98m,
                            WeekdayPrice = 172.83m,
                            CleaningFee = 23.55m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 14,
                            ConfirmationNumber = 21929,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3034) ?? throw new Exception("PropertyId 3034 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com") ?? throw new Exception("Customer orielly@foxnets.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 25),
                            EndDate = new DateTime(2024, 12, 28),
                            WeekendPrice = 260.62m,
                            WeekdayPrice = 111.73m,
                            CleaningFee = 15.89m,
                            DiscountRate = 0.24m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21929,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3099) ?? throw new Exception("PropertyId 3099 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com") ?? throw new Exception("Customer orielly@foxnets.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 29),
                            EndDate = new DateTime(2024, 12, 31),
                            WeekendPrice = 157.96m,
                            WeekdayPrice = 188.53m,
                            CleaningFee = 6.69m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21930,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3183) ?? throw new Exception("PropertyId 3183 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tanner@puppy.com") ?? throw new Exception("Customer tanner@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 29),
                            EndDate = new DateTime(2024, 12, 2),
                            WeekendPrice = 180.86m,
                            WeekdayPrice = 224.98m,
                            CleaningFee = 11.91m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 12,
                            ConfirmationNumber = 21931,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3127) ?? throw new Exception("PropertyId 3127 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "tuck33@puppy.com") ?? throw new Exception("Customer tuck33@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 14),
                            EndDate = new DateTime(2024, 12, 26),
                            WeekendPrice = 126.45m,
                            WeekdayPrice = 70.24m,
                            CleaningFee = 18.69m,
                            DiscountRate = 0.08m,
                            NumOfGuests = 4,
                            ConfirmationNumber = 21932,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3027) ?? throw new Exception("PropertyId 3027 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com") ?? throw new Exception("Customer fd@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 26),
                            EndDate = new DateTime(2024, 12, 31),
                            WeekendPrice = 119.06m,
                            WeekdayPrice = 189.98m,
                            CleaningFee = 25.11m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21932,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3122) ?? throw new Exception("PropertyId 3122 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com") ?? throw new Exception("Customer fd@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 4),
                            EndDate = new DateTime(2024, 12, 6),
                            WeekendPrice = 128.05m,
                            WeekdayPrice = 83.34m,
                            CleaningFee = 11.29m,
                            DiscountRate = 0.21m,
                            NumOfGuests = 8,
                            ConfirmationNumber = 21932,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3107) ?? throw new Exception("PropertyId 3107 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "fd@puppy.com") ?? throw new Exception("Customer fd@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 6),
                            EndDate = new DateTime(2024, 12, 10),
                            WeekendPrice = 139.83m,
                            WeekdayPrice = 155.03m,
                            CleaningFee = 21.05m,
                            DiscountRate = 0.09m,
                            NumOfGuests = 13,
                            ConfirmationNumber = 21933,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3065) ?? throw new Exception("PropertyId 3065 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "ra@aoo.com") ?? throw new Exception("Customer ra@aoo.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 12, 26),
                            EndDate = new DateTime(2025, 1, 5),
                            WeekendPrice = 212.70m,
                            WeekdayPrice = 212.86m,
                            CleaningFee = 6.82m,
                            DiscountRate = 0.00m,
                            NumOfGuests = 1,
                            ConfirmationNumber = 21934,
                            ReservationStatus = ResStatus.Valid,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3105) ?? throw new Exception("PropertyId 3105 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "louielouie@puppy.com") ?? throw new Exception("Customer louielouie@puppy.com not found.")
                        },
                        new Models.Reservation
                        {
                            StartDate = new DateTime(2024, 11, 25),
                            EndDate = new DateTime(2024, 11, 27),
                            WeekendPrice = 121.74m,
                            WeekdayPrice = 106.87m,
                            CleaningFee = 5.62m,
                            DiscountRate = 0.06m,
                            NumOfGuests = 6,
                            ConfirmationNumber = 21935,
                            ReservationStatus = ResStatus.Cancelled,
                            Property = db.Properties.FirstOrDefault(p => p.PropertyId == 3053) ?? throw new Exception("PropertyId 3053 not found."),
                            Customer = db.Users.FirstOrDefault(u => u.Email == "rwood@voyager.net") ?? throw new Exception("Customer rwood@voyager.net not found.")
                        }



                                    };

                    db.Reservations.AddRange(reservations);
                    db.SaveChanges();
                    transaction.Commit();

                    Console.WriteLine("Successfully seeded all reservations.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error seeding reservations: " + ex.Message);
                    throw;
                }
            }
        }
    }
    }



