using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task ssedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.productBrands != null && !context.productBrands.Any())
                {
                    var BrandsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    if(brands is not  null ) 
                        await context.productBrands.AddRangeAsync(brands);
                }
                if (context.productTypes != null && !context.productTypes.Any())
                {
                    var TypeData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                    if (Types is not null)
                        await context.productTypes.AddRangeAsync(Types);
                }
                if (context.products != null && !context.products.Any())
                {
                    var ProductData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Products>>(ProductData);

                    if (Products is not null)
                        await context.products.AddRangeAsync(Products);
                }
                await context.SaveChangesAsync();

                if (context.deliveryMethods != null && !context.deliveryMethods.Any())
                {
                    var DeliveryMethodFile = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
                    var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodFile);

                    if (deliveryMethod is not null)
                        await context.deliveryMethods.AddRangeAsync(deliveryMethod);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
