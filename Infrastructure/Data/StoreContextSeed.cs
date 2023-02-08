﻿using Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System     .Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public  static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var fileName = "../Infrastructure/Data/SeedData/brands.json";
                var brandsData = File.ReadAllText(fileName);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
                
            }

            if (!context.ProductTypes.Any())
            {
                var fileName = "../Infrastructure/Data/SeedData/types.json";
                var typesData = File.ReadAllText(fileName);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var fileName = "../Infrastructure/Data/SeedData/products.json";
                var productsData = File.ReadAllText(fileName);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        } 
    }
}
