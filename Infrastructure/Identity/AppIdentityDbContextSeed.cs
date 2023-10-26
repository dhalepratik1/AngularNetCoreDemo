using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Admin",
                    Email = "admin@gmail.com",
                    UserName =  "admin@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Admin",
                        LastName = "Administrator",
                        Street = "Pune Solapur Road",
                        City = "Pune",
                        State = "Maharashtra",
                        ZipCode = "411028"
                    }
                };
                await userManager.CreateAsync(user, "Admin@12");
            }
        }
    }
}