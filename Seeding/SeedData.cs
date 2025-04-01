using System;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;
namespace MIS333K_FinalProject.Seeding
{
    public static class SeedData // Add a static class
    {
        public static void SeedAllCategories(AppDbContext db)
        {
            // Define a list of categories to seed
            List<Category> categories = new List<Category>
            {
                new Category {  CategoryName = "House" },
                new Category {  CategoryName = "Apartment" },
                new Category {  CategoryName = "Condo" },
                new Category {  CategoryName = "Hotel" },
                new Category {  CategoryName = "Cabin" }
            };

            // Loop through the categories and add them if they don't already exist
            foreach (Category category in categories)
            {
                if (!db.Categories.Any(c => c.CategoryName == category.CategoryName))
                {
                    db.Categories.Add(category);
                }
            }

            // Save changes to the database
            db.SaveChanges();
            Console.WriteLine("Seeded all categories!");
        }

        //public static void SeedOneCategory(AppDbContext db)
        //{
        //    // Check if the category already exists to avoid duplicates
        //    if (!db.Categories.Any(c => c.CategoryName == "House"))
        //    {
        //        Category category = new Category
        //        {
        //            CategoryName = "House"
        //        };

        //        // Add the category to the database
        //        db.Categories.Add(category);
        //        db.SaveChanges();
        //        Console.WriteLine("Seeded one category: House");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Category already exists.");
        //    }
        //}

        
        }

    }

