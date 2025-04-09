using ECommerce.Models.Filters;
using ECommerce.Models.Models;

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
    }
}
