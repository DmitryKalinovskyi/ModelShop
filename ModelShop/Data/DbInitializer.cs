using System.Diagnostics;

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
    }
}
