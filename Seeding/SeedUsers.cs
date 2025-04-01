using Microsoft.AspNetCore.Identity;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;

namespace MIS333K_FinalProject.Seeding
{
    public static class SeedUsers
    {
        public static async Task SeedOneCustomer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            // Ensure the "Host" role exists
            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
                Console.WriteLine("Role 'Customer' created.");
            }

            // Host data with plain-text passwords
            var customers = new[]
            {
                    new { Email = "rwood@voyager.net", UserName = "rwood@voyager.net", Password = "drai494", LastName = "Wood", FirstName = "Reagan", MiddleInitial = "B", Address = "447 Westlake Dr.", ZipCode = "78746", PhoneNumber = "5124569229", Birthday = new DateTime(1988, 10, 24), HireStatus = false }
            };

            foreach (var customerData in customers)
            {
                // Check if the user already exists
                if (!db.Users.Any(u => u.Email == customerData.Email))
                {
                    // Create a new AppUser instance
                    var customer = new AppUser
                    {
                        Email = customerData.Email,
                        UserName = customerData.UserName,
                        LastName = customerData.LastName,
                        FirstName = customerData.FirstName,
                        MiddleInitial = customerData.MiddleInitial,
                        Address = customerData.Address,
                        ZipCode = customerData.ZipCode,
                        PhoneNumber = customerData.PhoneNumber,
                        Birthday = customerData.Birthday,
                        HireStatus = customerData.HireStatus
                    };

                    // Use the plain-text password during user creation
                    var result = await userManager.CreateAsync(customer, customerData.Password);

                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Seeded customer user: {customer.FirstName} {customer.LastName}");

                        // Assign the "Customer" role
                        var roleResult = await userManager.AddToRoleAsync(customer, "Customer");
                        if (roleResult.Succeeded)
                        {
                            Console.WriteLine($"Assigned role 'Customer' to {customer.Email}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to assign role 'Customer' to {customer.Email}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to seed customer user {customer.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Customer user already exists: {customerData.Email}");
                }
            }
        }

        public static async Task SeedAllCustomers(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            // Ensure the "Host" role exists
            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
                Console.WriteLine("Role 'Customer' created.");
            }

            // Host data with plain-text passwords
            var customers = new[]
            {
                    new { Email = "cbaker@freezing.co.uk", UserName = "cbaker@freezing.co.uk", Password = "hellothere", LastName = "Baker", FirstName = "Christopher", MiddleInitial = "L", Address = "1245 Lake America Blvd.", ZipCode = "78733", PhoneNumber = "5125595133", Birthday = new DateTime(1968, 11, 28), HireStatus = false },
                    new { Email = "mb@puppy.com", UserName = "mb@puppy.com", Password = "555533", LastName = "Bradicus", FirstName = "Michelle", MiddleInitial = "Q", Address = "1300 Small Pine Lane", ZipCode = "78261", PhoneNumber = "2102702860", Birthday = new DateTime(1988, 2, 7), HireStatus = false },
                    new { Email = "fd@puppy.com", UserName = "fd@puppy.com", Password = "666645", LastName = "Broccoli", FirstName = "Franco", MiddleInitial = "V", Address = "62 Cookie Rd", ZipCode = "77019", PhoneNumber = "8175683686", Birthday = new DateTime(1999, 11, 7), HireStatus = false },
                    new { Email = "wendy@puppy.com", UserName = "wendy@puppy.com", Password = "Kansas", LastName = "Charile", FirstName = "Wendy", MiddleInitial = "L", Address = "202 Bellmoth Hall", ZipCode = "78713", PhoneNumber = "5125967209", Birthday = new DateTime(1992, 10, 27), HireStatus = false },
                    new { Email = "limchou@puppy.com", UserName = "limchou@puppy.com", Password = "Rockwall", LastName = "Chou", FirstName = "Lim", MiddleInitial = "Q", Address = "1600 Barbara Lane", ZipCode = "78266", PhoneNumber = "2107748586", Birthday = new DateTime(1961, 11, 11), HireStatus = false },
                    new { Email = "444444.Dave@aool.com", UserName = "444444.Dave@aool.com", Password = "444444", LastName = "Dave", FirstName = "Shan", MiddleInitial = "D", Address = "234 Puppy Circle", ZipCode = "75208", PhoneNumber = "2142667242", Birthday = new DateTime(1972, 12, 19), HireStatus = false },
                    new { Email = "louann@puppy.com", UserName = "louann@puppy.com", Password = "longhorns", LastName = "Feeley", FirstName = "Lou Ann", MiddleInitial = "K", Address = "700 S 9th Street W", ZipCode = "77010", PhoneNumber = "8172580736", Birthday = new DateTime(1958, 8, 1), HireStatus = false },
                    new { Email = "tfreeley@puppy.com", UserName = "tfreeley@puppy.com", Password = "puppies", LastName = "Freeley", FirstName = "Tesa", MiddleInitial = "P", Address = "4334 Meanview Ave.", ZipCode = "77009", PhoneNumber = "8173279674", Birthday = new DateTime(2001, 7, 12), HireStatus = false },
                    new { Email = "mgar@puppy.com", UserName = "mgar@puppy.com", Password = "horses", LastName = "Garcia", FirstName = "Margaret", MiddleInitial = "L", Address = "594 Puppyview", ZipCode = "77003", PhoneNumber = "8176617531", Birthday = new DateTime(1956, 11, 17), HireStatus = false },
                    new { Email = "chaley@thug.com", UserName = "chaley@thug.com", Password = "mycats", LastName = "Harley", FirstName = "Charles", MiddleInitial = "E", Address = "One Ranger Pkwy", ZipCode = "75261", PhoneNumber = "2148499570", Birthday = new DateTime(1998, 5, 29), HireStatus = false },
                    new { Email = "wjhearniii@umch.edu", UserName = "wjhearniii@umch.edu", Password = "posicles", LastName = "Hearn", FirstName = "John", MiddleInitial = "B", Address = "4445 South First", ZipCode = "75237", PhoneNumber = "2148989608", Birthday = new DateTime(1983, 12, 29), HireStatus = false },
                    new { Email = "hicks43@puppy.com", UserName = "hicks43@puppy.com", Password = "guac45", LastName = "Hicks", FirstName = "Mark", MiddleInitial = "J", Address = "32 NE Mark Ln., Ste 910", ZipCode = "78239", PhoneNumber = "2105812952", Birthday = new DateTime(1989, 12, 16), HireStatus = false },
                    new { Email = "bradsingram@mall.utexas.edu", UserName = "bradsingram@mall.utexas.edu", Password = "father", LastName = "Ingram", FirstName = "Brad", MiddleInitial = "S", Address = "6548 La Chess St.", ZipCode = "78736", PhoneNumber = "5124702808", Birthday = new DateTime(1958, 9, 18), HireStatus = false },
                    new { Email = "father.Ingram@aool.com", UserName = "father.Ingram@aool.com", Password = "555897", LastName = "Jacobs", FirstName = "Todd", MiddleInitial = "L", Address = "4564 Palm St.", ZipCode = "78731", PhoneNumber = "5124677352", Birthday = new DateTime(1975, 12, 9), HireStatus = false },
                    new { Email = "victoria@puppy.com", UserName = "victoria@puppy.com", Password = "something", LastName = "Lawrence", FirstName = "Victoria", MiddleInitial = "M", Address = "6639 Butterfly Ln.", ZipCode = "78761", PhoneNumber = "5129481386", Birthday = new DateTime(1981, 5, 29), HireStatus = false },
                    new { Email = "lineback@flush.net", UserName = "lineback@flush.net", Password = "treelover", LastName = "Lineback", FirstName = "Brad", MiddleInitial = "W", Address = "1300 Pirateland St", ZipCode = "78293", PhoneNumber = "2102473963", Birthday = new DateTime(1973, 5, 19), HireStatus = false },
                    new { Email = "elowe@netscrape.net", UserName = "elowe@netscrape.net", Password = "headear", LastName = "Lowe", FirstName = "Evan", MiddleInitial = "S", Address = "3201 Pineapple Drive", ZipCode = "78279", PhoneNumber = "2105368614", Birthday = new DateTime(1993, 6, 7), HireStatus = false },
                    new { Email = "luce_chuck@puppy.com", UserName = "luce_chuck@puppy.com", Password = "gooseyloosey", LastName = "Luce", FirstName = "Chuck", MiddleInitial = "B", Address = "2345 Silent Clouds", ZipCode = "78268", PhoneNumber = "2107007535", Birthday = new DateTime(1995, 6, 11), HireStatus = false },
                    new { Email = "mackcloud@pimpdaddy.com", UserName = "mackcloud@pimpdaddy.com", Password = "rainyday", LastName = "MacLeod", FirstName = "Jennifer", MiddleInitial = "D", Address = "2504 Far East Blvd.", ZipCode = "78731", PhoneNumber = "5124772125", Birthday = new DateTime(1965, 10, 11), HireStatus = false },
                    new { Email = "liz@puppy.com", UserName = "liz@puppy.com", Password = "ember22", LastName = "Markham", FirstName = "Elizabeth", MiddleInitial = "P", Address = "7861 Chevy Mace Rd", ZipCode = "78732", PhoneNumber = "5124603832", Birthday = new DateTime(1989, 6, 18), HireStatus = false },
                    new { Email = "mclarence@puppy.com", UserName = "mclarence@puppy.com", Password = "lamemartin", LastName = "Martin", FirstName = "Clarence", MiddleInitial = "A", Address = "87 Alcedo St.", ZipCode = "77045", PhoneNumber = "8174979188", Birthday = new DateTime(1984, 4, 28), HireStatus = false },
                    new { Email = "lamemartin.Martin@aool.com", UserName = "lamemartin.Martin@aool.com", Password = "gregory", LastName = "Martinez", FirstName = "Gregory", MiddleInitial = "R", Address = "8295 Moon Blvd.", ZipCode = "77030", PhoneNumber = "8178770705", Birthday = new DateTime(1981, 12, 27), HireStatus = false },
                    new { Email = "cmiller@mapster.com", UserName = "cmiller@mapster.com", Password = "mucky44", LastName = "Miller", FirstName = "Charles", MiddleInitial = "R", Address = "8962 Side St.", ZipCode = "77031", PhoneNumber = "8177482602", Birthday = new DateTime(1987, 5, 5), HireStatus = false },
                    new { Email = "nelson.Kelly@puppy.com", UserName = "nelson.Kelly@puppy.com", Password = "Tree34", LastName = "Nelson", FirstName = "Kelly", MiddleInitial = "T", Address = "2601 Green River", ZipCode = "78703", PhoneNumber = "5122950953", Birthday = new DateTime(1969, 8, 3), HireStatus = false },
                    new { Email = "jojoe@puppy.com", UserName = "jojoe@puppy.com", Password = "jvb485bg", LastName = "Nguyen", FirstName = "Joe", MiddleInitial = "C", Address = "1249 4th NW St.", ZipCode = "75238", PhoneNumber = "2143149884", Birthday = new DateTime(1956, 2, 6), HireStatus = false },
                    new { Email = "orielly@foxnets.com", UserName = "orielly@foxnets.com", Password = "Bobbygirl", LastName = "O'Reilly", FirstName = "Bill", MiddleInitial = "T", Address = "8800 Gringo Drive", ZipCode = "78260", PhoneNumber = "2103474912", Birthday = new DateTime(1989, 3, 14), HireStatus = false },
                    new { Email = "or@puppy.com", UserName = "or@puppy.com", Password = "radioactive", LastName = "Radkovich", FirstName = "Anka", MiddleInitial = "L", Address = "1300 Freaky", ZipCode = "75260", PhoneNumber = "2142369553", Birthday = new DateTime(1952, 10, 26), HireStatus = false },
                    new { Email = "megrhodes@freezing.co.uk", UserName = "megrhodes@freezing.co.uk", Password = "gopigs", LastName = "Rhodes", FirstName = "Megan", MiddleInitial = "C", Address = "4587 Rightfield Rd.", ZipCode = "78707", PhoneNumber = "5123768733", Birthday = new DateTime(1958, 3, 18), HireStatus = false },
                    new { Email = "erynrice@puppy.com", UserName = "erynrice@puppy.com", Password = "iloveme", LastName = "Rice", FirstName = "Eryn", MiddleInitial = "M", Address = "3405 Rio Small", ZipCode = "78705", PhoneNumber = "5123900644", Birthday = new DateTime(2000, 11, 2), HireStatus = false },
                    new { Email = "jorge@hootmail.com", UserName = "jorge@hootmail.com", Password = "565656", LastName = "Rodriguez", FirstName = "Jorge", MiddleInitial = "", Address = "6788 Cotten Street", ZipCode = "77057", PhoneNumber = "8178928361", Birthday = new DateTime(1979, 1, 1), HireStatus = false },
                    new { Email = "ra@aoo.com", UserName = "ra@aoo.com", Password = "treeman", LastName = "Rogers", FirstName = "Allen", MiddleInitial = "B", Address = "4965 Rabbit Hill", ZipCode = "78732", PhoneNumber = "5128776930", Birthday = new DateTime(2000, 3, 12), HireStatus = false },
                    new { Email = "o_st-jean@home.com", UserName = "o_st-jean@home.com", Password = "55htrq", LastName = "Saint-Jean", FirstName = "Olivier", MiddleInitial = "M", Address = "255 Slap Dr.", ZipCode = "78292", PhoneNumber = "2104169665", Birthday = new DateTime(1997, 5, 1), HireStatus = false },
                    new { Email = "ss34@puppy.com", UserName = "ss34@puppy.com", Password = "leaves", LastName = "Saunders", FirstName = "Sarah", MiddleInitial = "J", Address = "332 Fish C", ZipCode = "78705", PhoneNumber = "5123521797", Birthday = new DateTime(1994, 5, 31), HireStatus = false },
                    new { Email = "willsheff@email.com", UserName = "willsheff@email.com", Password = "borbj44", LastName = "Sewell", FirstName = "William", MiddleInitial = "T", Address = "2365 34st St.", ZipCode = "78709", PhoneNumber = "5124534071", Birthday = new DateTime(1951, 12, 10), HireStatus = false },
                    new { Email = "sheff44@puppy.com", UserName = "sheff44@puppy.com", Password = "ldiul485", LastName = "Sheffield", FirstName = "Martin", MiddleInitial = "J", Address = "3886 Road A", ZipCode = "78705", PhoneNumber = "5125503154", Birthday = new DateTime(1993, 7, 2), HireStatus = false },
                    new { Email = "johnsmith187@puppy.com", UserName = "johnsmith187@puppy.com", Password = "kribv75", LastName = "Smith", FirstName = "John", MiddleInitial = "A", Address = "23 Known Forge Dr.", ZipCode = "78280", PhoneNumber = "2108345875", Birthday = new DateTime(1985, 6, 13), HireStatus = false },
                    new { Email = "jeff@puppy.com", UserName = "jeff@puppy.com", Password = "jeffery", LastName = "Stark", FirstName = "Jeffrey", MiddleInitial = "T", Address = "337 40th St.", ZipCode = "78705", PhoneNumber = "5127002600", Birthday = new DateTime(1974, 5, 2), HireStatus = false },
                    new { Email = "dustroud@mail.com", UserName = "dustroud@mail.com", Password = "klavjkb48", LastName = "Stroud", FirstName = "Dustin", MiddleInitial = "P", Address = "1212 Henrietta Rd", ZipCode = "75221", PhoneNumber = "2142370654", Birthday = new DateTime(1974, 7, 14), HireStatus = false },
                    new { Email = "eric_stuart@puppy.com", UserName = "eric_stuart@puppy.com", Password = "vkb451", LastName = "Stuart", FirstName = "Eric", MiddleInitial = "D", Address = "5576 Big Ring", ZipCode = "78746", PhoneNumber = "5128202322", Birthday = new DateTime(1968, 6, 17), HireStatus = false },
                    new { Email = "peterstump@hootmail.com", UserName = "peterstump@hootmail.com", Password = "kdsiu4", LastName = "Stump", FirstName = "Peter", MiddleInitial = "L", Address = "1300 Kellen Square", ZipCode = "77018", PhoneNumber = "8174584890", Birthday = new DateTime(2001, 7, 23), HireStatus = false },
                    new { Email = "tanner@puppy.com", UserName = "tanner@puppy.com", Password = "klrfbj45", LastName = "Tanner", FirstName = "Jeremy", MiddleInitial = "S", Address = "4347 Palmstead", ZipCode = "77044", PhoneNumber = "8174614916", Birthday = new DateTime(1973, 12, 28), HireStatus = false },
                    new { Email = "taylordjay@puppy.com", UserName = "taylordjay@puppy.com", Password = "lraggrhb854", LastName = "Taylor", FirstName = "Allison", MiddleInitial = "R", Address = "467 Nueces St.", ZipCode = "78705", PhoneNumber = "5124772439", Birthday = new DateTime(1999, 9, 30), HireStatus = false },
                    new { Email = "lraggrhb854.Taylor@aool.com", UserName = "lraggrhb854.Taylor@aool.com", Password = "alsuib95", LastName = "Taylor", FirstName = "Rachel", MiddleInitial = "K", Address = "345 Shortview Dr.", ZipCode = "78705", PhoneNumber = "5124536618", Birthday = new DateTime(1956, 2, 24), HireStatus = false },
                    new { Email = "tee_frank@hootmail.com", UserName = "tee_frank@hootmail.com", Password = "kd1734", LastName = "Tee", FirstName = "Frank", MiddleInitial = "J", Address = "5590 Big Dr.", ZipCode = "77004", PhoneNumber = "8178789530", Birthday = new DateTime(1964, 11, 11), HireStatus = false },
                    new { Email = "tuck33@puppy.com", UserName = "tuck33@puppy.com", Password = "kjdb983", LastName = "Tucker", FirstName = "Clent", MiddleInitial = "J", Address = "3132 Main St.", ZipCode = "75315", PhoneNumber = "2148495141", Birthday = new DateTime(1990, 6, 25), HireStatus = false },
                    new { Email = "avelasco@puppy.com", UserName = "avelasco@puppy.com", Password = "odrb02", LastName = "Velasco", FirstName = "Allen", MiddleInitial = "G", Address = "634 W. 4th", ZipCode = "75207", PhoneNumber = "2144009625", Birthday = new DateTime(1966, 12, 13), HireStatus = false },
                    new { Email = "westj@pioneer.net", UserName = "westj@pioneer.net", Password = "kndl847", LastName = "West", FirstName = "Jake", MiddleInitial = "T", Address = "RR 3244", ZipCode = "75323", PhoneNumber = "2148499231", Birthday = new DateTime(1968, 2, 6), HireStatus = false },
                    new { Email = "louielouie@puppy.com", UserName = "louielouie@puppy.com", Password = "lb2394", LastName = "Winthorpe", FirstName = "Louis", MiddleInitial = "L", Address = "2500 Madre Blvd", ZipCode = "78746", PhoneNumber = "2145674085", Birthday = new DateTime(1961, 7, 23), HireStatus = false },
                    new { Email = "rwood@voyager.net", UserName = "rwood@voyager.net", Password = "drai494", LastName = "Wood", FirstName = "Reagan", MiddleInitial = "B", Address = "447 Westlake Dr.", ZipCode = "78746", PhoneNumber = "5124569229", Birthday = new DateTime(1988, 10, 24), HireStatus = false }
            };

            foreach (var customerData in customers)
            {
                // Check if the user already exists
                if (!db.Users.Any(u => u.Email == customerData.Email))
                {
                    // Create a new AppUser instance
                    var customer = new AppUser
                    {
                        Email = customerData.Email,
                        UserName = customerData.UserName,
                        LastName = customerData.LastName,
                        FirstName = customerData.FirstName,
                        MiddleInitial = customerData.MiddleInitial,
                        Address = customerData.Address,
                        ZipCode = customerData.ZipCode,
                        PhoneNumber = customerData.PhoneNumber,
                        Birthday = customerData.Birthday,
                        HireStatus = customerData.HireStatus
                    };

                    // Use the plain-text password during user creation
                    var result = await userManager.CreateAsync(customer, customerData.Password);

                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Seeded customer user: {customer.FirstName} {customer.LastName}");

                        // Assign the "Customer" role
                        var roleResult = await userManager.AddToRoleAsync(customer, "Customer");
                        if (roleResult.Succeeded)
                        {
                            Console.WriteLine($"Assigned role 'Customer' to {customer.Email}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to assign role 'Customer' to {customer.Email}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to seed customer user {customer.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Customer user already exists: {customerData.Email}");
                }
            }
        }
    }
}

