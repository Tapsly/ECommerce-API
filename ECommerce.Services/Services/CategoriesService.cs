using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Caching;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Services.Services
{
    public class CategoriesService(ECommerceDbContext dbContext, IMemoryCache cache) : ICategory
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        private readonly CategoriesCache _cache = new(cache);
        public async Task<List<Category>> CreateCategoryAsync(Category category)
        {
            // first check if the category exists in cache
            var categories = _cache.GetValues("categories");
            try
            {
                if (categories is not null && !categories!.Contains(category))
                {
                    var categoryExists = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                    if (categoryExists == null)
                    {
                        _dbContext.Categories.Add(category);
                        await _dbContext.SaveChangesAsync();
                        categories = await GetCategoriesAsync();
                    }
                }
                return categories!;
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
                _cache.ClearCache("categories");
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
                    // clear the cache since there is a change in the categories
                    _cache.ClearCache("categories");
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
                // get the categories from the cache first
                var categories = _cache.GetValues("categories");
                // if no categories in cache, get from the database, and then add in cache
                categories ??= await _dbContext.Categories.AsNoTracking().ToListAsync();
                cache.Set(categories, "categories");
                return categories;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            Category? category = new();
            try
            {
                // look for the category from the cache first
                var categories = _cache.GetValues("categories");
                if (categories is not null)
                {
                    category = (Category)categories!.Where(c => c.Id == id);
                    if (category is not null)
                    {
                        return category;
                    }
                    else
                    {
                        category = await _dbContext.Categories.FindAsync(id);
                        if (category is not null)
                        {
                            // cache might be outdated, clear cache
                            _cache.ClearCache("categories");
                        }
                    }
                }
                return category;
            }
            catch (ArgumentNullException)
            {

                throw;
            }

        }

        public async Task<Category> UpdateCategoryByIdAsync(int id, Category updatedCategory)
        {
            try
            {
                // compare the updated category with the one in the cache first
                var categories = _cache.GetValues("categories");
                if (categories is not null)
                {
                    var existingCategory = categories.Where(c => c.Id == id).FirstOrDefault();
                    if (existingCategory is not null && existingCategory.Equals(updatedCategory))
                    {
                        return existingCategory;
                    }
                    // if not the same, clear cache because with this update
                    // cache will be outdated
                    _cache.ClearCache("categories");
                    _dbContext.Categories.Entry(existingCategory!).CurrentValues.SetValues(updatedCategory);
                    await _dbContext.SaveChangesAsync();
                    // fetch and cache the updated categories data
                    _cache.SetValues(await _dbContext.Categories.ToListAsync(), "categories");
                    return updatedCategory;
                }
                return updatedCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
