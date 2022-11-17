using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext dataContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!dataContext.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("F:\\Coding\\.Net\\eShoppingZone\\Infrastructure\\Data\\SeedData\\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach(var item in brands)
                    {
                        dataContext.ProductBrands.Add(item);
                    }

                    await dataContext.SaveChangesAsync();
                }

                if (!dataContext.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("F:\\Coding\\.Net\\eShoppingZone\\Infrastructure\\Data\\SeedData\\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        dataContext.ProductTypes.Add(item);
                    }

                    await dataContext.SaveChangesAsync();
                }

                if (!dataContext.Products.Any())
                {
                    var productsData = File.ReadAllText("F:\\Coding\\.Net\\eShoppingZone\\Infrastructure\\Data\\SeedData\\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        dataContext.Products.Add(item);
                    }

                    await dataContext.SaveChangesAsync();
                }

                if (!dataContext.DeliveryMethods.Any())
                {
                    var dmData = File.ReadAllText("F:\\Coding\\.Net\\eShoppingZone\\Infrastructure\\Data\\SeedData\\delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        dataContext.DeliveryMethods.Add(item);
                    }

                    await dataContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
