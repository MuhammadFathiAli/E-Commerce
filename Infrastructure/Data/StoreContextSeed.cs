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

                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                    {
                        context.ProductTypes.Add(type);
                    }
                     await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
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
