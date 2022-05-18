using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync (IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<StoreContext>();

                context.Database.EnsureCreated();

                if (!context.Categories.Any())
                {

                    //var brandsData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
                    //var brands = JsonSerializer.Deserialize<List<Category>>(brandsData);
                    var brands = new List<Category>
                    {
                        new Category { Title = "Shirt", Description ="Best T-Shirts ForEver" },
                        new Category { Title = "Dresses", Description ="Best Dresses ForEver" },
                        new Category { Title = "Skirts", Description ="Best Skirts ForEver" },
                        new Category { Title = "Short", Description ="Best Short ForEver" },
                        new Category { Title = "Jackets", Description ="Best Jackets ForEver" },
                        new Category { Title = "Coats", Description ="Best Coats ForEver" },
                        new Category { Title = "Bags", Description ="Best Bags ForEver" },
                        new Category { Title = "Accessories", Description ="Best Accessories ForEver" },
                        new Category { Title = "Scarfs", Description ="Best Scarfs ForEver"},
                        new Category { Title = "Shoeses", Description ="Best Shoeses ForEver" },
                     };
                    foreach (var brand in brands)
                    {
                        context.Categories.Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    //var productsData = File.ReadAllText("../Infrastructure/SeedData/products.json");
                    //var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    var products = new List<Product>
                    {
                        new Product { Title ="Product3", CategoryId = 1, Description="Prd3", Image="assets/images/products/product-3.jpg", Price=300, Quantity=10},
                        new Product { Title ="Product4", CategoryId = 2, Description="Prd4", Image="assets/images/products/product-4.jpg", Price=400, Quantity=10},
                        new Product { Title ="Product5", CategoryId = 3, Description="Prd5", Image="assets/images/products/product-5.jpg", Price=500, Quantity=10},
                        new Product { Title ="Product6", CategoryId = 1, Description="Prd6", Image="assets/images/products/product-6.jpg", Price=600, Quantity=10},
                        new Product { Title ="Product7", CategoryId = 4, Description="Prd7", Image="assets/images/products/product-7.jpg", Price=700, Quantity=10},
                        new Product { Title ="Product8", CategoryId = 5, Description="Prd8", Image="assets/images/products/product-8.jpg", Price=800, Quantity=10},
                        new Product { Title ="Product9", CategoryId = 6, Description="Prd9", Image="assets/images/products/product-9.jpg", Price=900, Quantity=10},
                        new Product { Title ="Product10", CategoryId = 7, Description="Prd10", Image="assets/images/products/product-10.jpg", Price=350, Quantity=10},
                        new Product { Title ="Product11", CategoryId = 8, Description="Prd11", Image="assets/images/products/product-11.jpg", Price=450, Quantity=10},
                        new Product { Title ="Product12", CategoryId = 1, Description="Prd12", Image="assets/images/products/product-12.jpg", Price=650, Quantity=10},
                    };
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                   await context.SaveChangesAsync();
                }
      
                if (!context.DeliveryMethods.Any())
                {
                    var dmsData = File.ReadAllText("../Infrastructure/SeedData/delivery.json");
                    var dms = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmsData);
                    foreach (var dm in dms)
                    {
                        context.DeliveryMethods.Add(dm);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
