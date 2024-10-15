using Microsoft.EntityFrameworkCore;
using Eksamen.Models;

namespace Eksamen.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ProductDbContext context = serviceScope.ServiceProvider.GetRequiredService<ProductDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Kyllingvinger",
                    ProducerID = 1,
                    Energy = 716.0m,
                    Fat = 11.0m,
                    Saturates = 3.3m,
                    Carbs = 0.7m,
                    Protein = 17.30m,
                    Salt = 0.80m,
                    ImageUpload = "/images/cider.jpg"

                },
                new Product
                {
                    Name = "Kyllingfilet",
                    ProducerID = 1,
                    Energy = 469.0m,
                    Fat = 2.10m,
                    Saturates = 0.50m,
                    Carbs = 0.7m,
                    Protein = 23.00m,
                    Salt = 0.10m,
                    ImageUpload = "/images/pizza.jpg"

                }
            };
            context.AddRange(products);
            context.SaveChanges();
        }
        context.SaveChanges();
    }
}