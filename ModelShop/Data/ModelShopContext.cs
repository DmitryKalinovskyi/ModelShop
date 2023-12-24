using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelShop.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ModelShop.Data
{
    public class ModelShopContext : IdentityDbContext<Client>
    {
        public ModelShopContext(DbContextOptions<ModelShopContext> options)
            : base(options)
        {
            
        }

        public DbSet<Model3D> Models3D { get; set; }
        
        public DbSet<ModelCategory> ModelCategories { get; set; }
        
        public DbSet<Client> Clients{ get; set; }
        
        public DbSet<Cart> Carts{ get; set; }

        public DbSet<Cart_Model3D> Cart_Models3D{ get; set; }


        //public DbSet<ModelFile> ModelFiles{ get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ModelCategory>().ToTable("ModelCategories");

        //    modelBuilder.Entity<Model3D>().ToTable("Models3D");
        //       //.HasOne(m => m.ModelCategory)
        //       //.WithMany(mc => mc.Models)
        //       //.HasForeignKey(m => m.ModelCategoryID);
        //}
    }
}
