using InternetShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Data.Context;

public class InternetShopContext : DbContext
{
    public InternetShopContext(DbContextOptions<InternetShopContext> options) : base(options) { }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);

       modelBuilder.Entity<Product>().HasData(
           new Product { ProductId = 1, Title = "IPhone", Description = "IPhone description", Price = 39.99M },
           new Product { ProductId = 2, Title = "HTC",Description = "HTC description", Price = 19.99M }
       );
   }
}