using ECommerce.Dtos;
using ECommerce.Models.Models;

namespace ECommerce.MappingProfiles
{
    public class ProductMappingProfiles
    {
        public static Product MapToProduct(ProductCreateDto productCreateDTO)
        {
            return new Product
            {
                Name = productCreateDTO.Name,
                Price = productCreateDTO.Price,
                Description = productCreateDTO.Description,
                StockQuantity = productCreateDTO.Stock,
                CategoryId = productCreateDTO.CategoryId,
            };
        }

        public static ProductDto MapToProductDTO(Product product)
        {
            return new ProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                DiscountPercentage = product.DiscountPercentage,
                CategoryId = product.CategoryId,
            };
        }

        public static ProductDetailsDto MapToProductDetailsDTO(Product product)
        {
            return new ProductDetailsDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                DiscountPercentage = product.DiscountPercentage,
                SKU = product.SKU,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
            };
        }
    }
}
