using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Eksamen.Models;

public class ProductDbContext : IdentityDbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }
    public DbSet<Product> Products { get; set;}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}