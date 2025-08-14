using InternetShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Data.Context;

public class InternetShopContext : DbContext
{
    public InternetShopContext(DbContextOptions<InternetShopContext> options) : base(options) { }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

   // public virtual DbSet<Session> Sessions { get; set; }
}