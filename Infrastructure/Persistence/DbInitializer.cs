using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _dbContext;
        public DbInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       

        public async Task InitializeAsync()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types != null && types.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(types);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands != null && brands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(brands);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products != null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                        await _dbContext.SaveChangesAsync();

                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database seeding failed", ex);
            }
        } 
    }
    
}
