using Microsoft.AspNetCore.Identity;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using System.Diagnostics;
using System.Net;

namespace ModelShop.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ModelShopContext context)
        {
        }

        public static async Task SeedClientsAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Client>>();
                string adminUserEmail = "test@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Client()
                    {
                        UserName = "deeperxd",
                        Firstname = "Dmitry",
                        Lastname = "Kalinovskyi",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Description = "Admin profile",
                        Cart = new Cart()
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
