using Microsoft.AspNetCore.Identity;
using ModelShop.Models;
using System.Diagnostics;
using System.Net;

namespace ModelShop.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ModelShopContext context)
        {
            //// Look for any students.
            //if (context.Students.Any())
            //{
            //    return;   // DB has been seeded
            //}

            if (context.Models3D.Any())
            {
                return;
            }

            // Initialize with models

            //var models = new List<Models.Model3D>
            //{
            //    new Models.Model3D{ Title="A", Description = "Model 1!", CreatedDate = DateTime.Now, ModelCategoryID=1},
            //    new Models.Model3D{ Title="B", Description = "Model 2!", CreatedDate = DateTime.Now, ModelCategoryID=1},
            //    new Models.Model3D{ Title="C", Description = "Model 3!", CreatedDate = DateTime.Now, ModelCategoryID=1},
            //};

            //context.Models3D.AddRange(models);
            //context.SaveChanges();
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
                        Description = "Admin profile"

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
