using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using ECommerce.Utils.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class ProductsService(ECommerceDbContext dbContext):IProduct
    {
        private readonly ECommerceDbContext _dbContext = dbContext;

        public async Task CreateProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductsAsync(GetProductFilter filter)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.Name.Contains(filter.Name));
            }
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }
            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }
            // Add query for the search according to category
            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetProductsPagedAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task UpdateProductPriceByIdAsync(int id, decimal price)
        {
            var existingProduct = await _dbContext.Products.FindAsync(id);
            if (existingProduct != null)
            {
                existingProduct.Price = price;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProductByIdAsync(int id, Product product)
        {
            var existingProduct = await _dbContext.Products.FindAsync(id);

            if (existingProduct != null)
            {
                _dbContext.Products.Entry(existingProduct).CurrentValues.SetValues(product);
                await _dbContext.SaveChangesAsync();
            }
        }


        public Task UpLoadProductImageAsync(int id, IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}
