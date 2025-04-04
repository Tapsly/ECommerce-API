using ECommerce.Models.Models;
using ECommerce.Dtos;
namespace ECommerce.MappingProfiles
{
    public class CategoryMappingProfiles
    {
        public static Category MapToCategory(CategoryCreateDto categoryCreateDTO)
        {
            return new Category
            {
                Name = categoryCreateDTO.Name,
                Description = categoryCreateDTO.Description,
            };
        }
        public static CategoryDetailsDto MapToCategoryDetailsDTO(Category category)
        {
            return new CategoryDetailsDto
            {
                Name = category.Name,
                Description = category!.Description!,
                Products = category.Products,
            };
        }
        public static CategoryDto MapToCategoryDTO(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}
