using Core.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync (IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var identitycontext = serviceScope.ServiceProvider.GetService<AppIdentityDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                identitycontext.Database.EnsureCreated();
               
                if (!userManager.Users.Any())
                {
                    var user = new AppUser()
                    {
                        DisplayName = "iti",
                        Email = "iti@test.com",
                        UserName = "iti@test.com",
                        Address = new Address()
                        {
                            FirstName = "iti",
                            LastName = "SWA",
                            Street = "14",
                            City = "Cairo",
                        }
                    };
                    await userManager.CreateAsync(user, "Coding@1234");
                }
            }
        }
    }
}
