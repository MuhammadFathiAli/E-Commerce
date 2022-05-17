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
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                identitycontext.Database.EnsureCreated();


                //roles
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


                //users
                //1-    admin user
                var adminUserEmail = "admin@test.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newadminUser = new AppUser
                    {
                        DisplayName = "Admin User",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        UserName = "Admin"
                    };
                    await userManager.CreateAsync(newadminUser, "Coding@1234");
                    await userManager.AddToRoleAsync(newadminUser, UserRoles.Admin);
                }

                //2-    app user

                var appUserEmail = "iti@test.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppuser = new AppUser
                    {
                        DisplayName = "iti App User",
                        Email = appUserEmail,
                        UserName = "iti",
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            FirstName = "iti",
                            LastName = "SWA",
                            Street = "14",
                            City = "Cairo",
                        }
                    };
                    await userManager.CreateAsync(newAppuser, "Coding@1234");
                    await userManager.AddToRoleAsync(newAppuser, UserRoles.User);
                }
            }
        }
    }
}
