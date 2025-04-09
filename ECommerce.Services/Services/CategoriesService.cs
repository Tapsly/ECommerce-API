using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Caching;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services
{
    public class CategoriesService(ECommerceDbContext dbContext) : ICategory
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        private readonly CategoriesCache? cache;
        public async Task<List<Category>> CreateCategoryAsync(Category category)
        {
            // first check if the category exists in cache
            var categories = cache!.GetValues("categories");
            try
            {
                if (categories is not null && !categories!.Contains(category))
                {
                    var categoryExists = _dbContext.Categories.FirstOrDefault(c => c.Name == category.Name);
                    if (categoryExists != null)
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
                cache!.ClearCache("categories");
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
                    cache!.ClearCache("categories");
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
                var categories = cache!.GetValues("categories");
                // if no categories in cache, get from the database, and then add in cache
                categories ??= await _dbContext.Categories.AsNoTracking().ToListAsync();
                cache.SetValues(categories, "categories");
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
                var categories = cache!.GetValues("categories");
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
                            cache!.ClearCache("categories");
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
                var categories = cache!.GetValues("categories");
                if (categories is not null)
                {
                    var existingCategory = categories.Where(c => c.Id == id).FirstOrDefault();
                    if (existingCategory is not null && existingCategory.Equals(updatedCategory))
                    {
                        return existingCategory;
                    }
                    // if not the same, clear cache because with this update
                    // cache will be outdated
                    cache.ClearCache("categories");
                    _dbContext.Categories.Entry(existingCategory!).CurrentValues.SetValues(updatedCategory);
                    await _dbContext.SaveChangesAsync();
                    // fetch and cache the updated categories data
                    cache.SetValues(await _dbContext.Categories.ToListAsync(), "categories");
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
