using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class CategoriesService(ECommerceDbContext dbContext):ICategory
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        public async Task CreateCategoryAsync(Category category)
        {
            var categoryExists = _dbContext.Categories.FirstOrDefault(c => c.Name == category.Name);
            try
            {
                if (categoryExists != null)
                {
                    _dbContext.Categories.Add(category);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCategoriesAsync()
        {
            try
            {
                await _dbContext.Categories.ExecuteDeleteAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCategoryByIdAsync(int id)
        {
            var existingCategory = _dbContext.Categories.Find(id);

            try
            {
                if (existingCategory != null)
                {
                    _dbContext.Categories.Remove(existingCategory);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                return await _dbContext.Categories.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(id);
            return existingCategory!;
        }

        public async Task UpdateCategoryByIdAsync(int id, Category updatedCategory)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(id);
            try
            {
                _dbContext.Categories.Entry(existingCategory!).CurrentValues.SetValues(updatedCategory);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
