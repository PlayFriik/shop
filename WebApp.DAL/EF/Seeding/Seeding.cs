using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Domain;
using WebApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DAL.EF.Seeding
{
    public static class Seeding
    {
        public static void DatabaseDrop(AppDbContext appDbContext)
        {
            appDbContext.Database.EnsureDeleted();
        }

        public static void DatabaseMigrate(AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();
        }

        public static void SeedData(AppDbContext appDbContext)
        {
            var caps = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Caps"
            };
            appDbContext.Categories.Add(caps);

            var cups = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Cups"
            };
            appDbContext.Categories.Add(cups);

            var masks = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Masks"
            };
            appDbContext.Categories.Add(masks);
            
            var shakers = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Shakers"
            };
            appDbContext.Categories.Add(shakers);
            
            var shoes = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Shoes"
            };
            appDbContext.Categories.Add(shoes);

            var cap = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = caps.Id,
                Name = "Cap",
                Description = "Have you ever wanted a cap? No? Very good! Because NOW there is a cap that will change your mind! The cap is made of strongly brushed cotton and its size can be adjusted with a velcro strap.",
                Price = 14,
                Quantity = 65,
                Sold = 35
            };
            appDbContext.Products.Add(cap);
            
            var cup = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = cups.Id,
                Name = "Cup",
                Description = "History has shown that liquids need a container. Here is one option. There is plenty of space between the handle and the cup itself to make your drinking experience better.",
                Price = 12,
                Quantity = 60,
                Sold = 40
            };
            appDbContext.Products.Add(cup);

            var mask = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = masks.Id,
                Name = "Face Mask (5 pcs)",
                Description = "Going out? Stay safe and mask up!",
                Price = 15,
                Quantity = 70,
                Sold = 30
            };
            appDbContext.Products.Add(mask);
            
            var shaker = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = shakers.Id,
                Name = "Shaker",
                Description = "High quality 500ml shaker that helps you stay hydrated regardless whether you are outdoors or at home.",
                Price = 25,
                Quantity = 75,
                Sold = 25
            };
            appDbContext.Products.Add(shaker);
            
            var converse = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = shoes.Id,
                Name = "Converse",
                Description = "-",
                Price = 75,
                Quantity = 95,
                Sold = 5
            };
            appDbContext.Products.Add(converse);

            var itella = new Provider
            {
                Id = Guid.NewGuid(),
                Name = "Itella",
                Price = 2
            };
            appDbContext.Providers.Add(itella);

            var omniva = new Provider
            {
                Id = Guid.NewGuid(),
                Name = "Omniva",
                Price = 3
            };
            appDbContext.Providers.Add(omniva);

            var kehra = new Location
            {
                Id = Guid.NewGuid(),
                ProviderId = itella.Id,
                Provider = itella,
                Name = "Kehra Grossi",
                Address = "Kose 7, Kehra, 74305 Harju"
            };
            appDbContext.Locations.Add(kehra);

            var keila = new Location
            {
                Id = Guid.NewGuid(),
                ProviderId = itella.Id,
                Provider = itella,
                Name = "Keila Grossi",
                Address = "Piiri 7, Keila, 76606 Harju"
            };
            appDbContext.Locations.Add(keila);

            var abja = new Location
            {
                Id = Guid.NewGuid(),
                ProviderId = omniva.Id,
                Provider = omniva,
                Name = "Abja Coop Konsum",
                Address = "Pärnu 13, Abja-Paluoja, 69403 Viljandi"
            };
            appDbContext.Locations.Add(abja);

            var ahtme = new Location
            {
                Id = Guid.NewGuid(),
                ProviderId = omniva.Id,
                Provider = omniva,
                Name = "Ahtme Maxima XX",
                Address = "Puru 77, 31021 Ida-Viru"
            };
            appDbContext.Locations.Add(ahtme);

            var aseri = new Location
            {
                Id = Guid.NewGuid(),
                ProviderId = omniva.Id,
                Provider = omniva,
                Name = "Aseri Grossi",
                Address = "Tehase 23, Aseri, 43401 Ida-Viru"
            };
            appDbContext.Locations.Add(aseri);
            
            var processing = new Status
            {
                Id = Guid.NewGuid(),
                Name = "Processing"
            };
            appDbContext.Statuses.Add(processing);

            var readyToShip = new Status
            {
                Id = Guid.NewGuid(),
                Name = "Ready to ship"
            };
            appDbContext.Statuses.Add(readyToShip);

            var shipped = new Status
            {
                Id = Guid.NewGuid(),
                Name = "Shipped"
            };
            appDbContext.Statuses.Add(shipped);

            appDbContext.SaveChanges();
        }
        
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, string email, string password)
        {
            var appRole = new AppRole
            {
                Name = "Admin"
            };

            var result = roleManager.CreateAsync(appRole).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            
            appRole = new AppRole
            {
                Name = "Seller"
            };

            result = roleManager.CreateAsync(appRole).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }

            var appUser = new AppUser
            {
                UserName = email,
                Email = email,
                PhoneNumber = "00000000",
                FirstName = "Administrator",
                LastName = "account"
            };

            result = userManager.CreateAsync(appUser, password).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }

            result = userManager.AddToRolesAsync(appUser, new[] { "Admin", "Seller" }).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
        }
    }
}
