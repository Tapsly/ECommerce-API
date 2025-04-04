using ECommerce.Models.Models;
using ECommerce.Utils.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface IProduct
    {
        public Task<Product?> GetProductByIdAsync(int id);
        public Task<List<Product>> GetProductsAsync(GetProductFilter filter);
        public Task CreateProductAsync(Product product);
        public Task UpdateProductByIdAsync(int id, Product product);
        public Task UpdateProductPriceByIdAsync(int id, decimal price);
        public Task<List<Product>> GetProductsPagedAsync(int pageNumber, int pageSize);
        public Task UpLoadProductImageAsync(int id, IFormFile formFile);
    }
}
