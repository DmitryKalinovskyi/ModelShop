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

        public DbSet<Order> Orders { get; set; }

        public DbSet<Order_Model3D> Client_Models3D { get; set; }

        public DbSet<ClientFollower> ClientFollowers { get; set; }
    }
}
