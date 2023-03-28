using Core.Entities.Identity;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Vik",
                    Email = "vik@test.com",
                    UserName = "vik@test.com",
                    Address = new Address
                    {
                        FirstName = "Vik",
                        LastName = "Banky",
                        Street = "10, The Street",
                        City = "Ikeja",
                        State = "Lagos",
                        ZipCode = "10027"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
        
    }
}
