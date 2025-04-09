using ECommerce.Models.Models;

namespace ECommerce.Services.Interfaces
{
    public interface ICategory
    {
        public Task<List<Category>> GetCategoriesAsync();
        public Task<Category?> GetCategoryByIdAsync(int id);
        public Task<List<Category>> CreateCategoryAsync(Category category);
        public Task<Category> UpdateCategoryByIdAsync(int id, Category updatedCategory);
        public Task DeleteCategoriesAsync();
        public Task DeleteCategoryByIdAsync(int id);
    }
}
