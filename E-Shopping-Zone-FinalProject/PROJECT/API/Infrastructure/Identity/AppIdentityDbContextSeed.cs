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
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Shubh",
                    Email = "shubh@test.com",
                    UserName = "shubh@test.com",
                    Address = new Address
                    {
                        FirstName = "Shubh",
                        LastName = "Shukla",
                        Street = "Ganesh Sqaure",
                        City = "Jhansi",
                        State = "UP",
                        PinCode = "767039"
                    }
                };

                await userManager.CreateAsync(user, "Shubh100@");
            }
        }
    }
}
