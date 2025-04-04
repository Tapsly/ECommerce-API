using ECommerce.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface ICategory
    {
        public Task<List<Category>> GetCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task CreateCategoryAsync(Category category);
        public Task UpdateCategoryByIdAsync(int id, Category updatedCategory);
        public Task DeleteCategoriesAsync();
        public Task DeleteCategoryByIdAsync(int id);
    }
}
