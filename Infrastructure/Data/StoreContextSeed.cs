using Core.Entities;
using Core.Entities.OrderAggregate;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public  static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.ProductBrands.Any())
            {
                var fileName = path + @"/Data/SeedData/brands.json";
                var brandsData = File.ReadAllText(fileName);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
                
            }

            if (!context.ProductTypes.Any())
            {
                var fileName = path + @"/Data/SeedData/types.json";
                var typesData = File.ReadAllText(fileName);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var fileName = path + @"/Data/SeedData/products.json";
                var productsData = File.ReadAllText(fileName);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (!context.DeliveryMethods.Any())
            {
                var fileName = path + @"/Data/SeedData/delivery.json";
                var deliveryData = File.ReadAllText(fileName);
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(methods);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        } 
    }
}
