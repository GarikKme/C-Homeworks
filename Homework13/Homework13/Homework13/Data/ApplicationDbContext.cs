using Homework13.Models;
using Microsoft.EntityFrameworkCore;
namespace Homework13.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<User> Users { get; set; }
}