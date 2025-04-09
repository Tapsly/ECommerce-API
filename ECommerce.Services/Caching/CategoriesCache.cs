/*
 *  This file contains code that is used for any caching related operations
 *  The code within this module must be reusable, cache methods must not be 
 *  designed to a specific data type.
 */

using ECommerce.Models.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Services.Caching
{
    public class CategoriesCache(IMemoryCache cache)
    {
        private readonly IMemoryCache _cache = cache;

        public List<Category>? GetValues(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }
            var myCategories = new List<Category>();
            try
            {
                if (_cache.TryGetValue(cacheKey, out List<Category>? categories))
                {
                    myCategories = categories;
                }

                return myCategories;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Category> SetValues(List<Category> category, string cacheKey)
        {
            if (category == null || string.IsNullOrEmpty(cacheKey))
            {
                throw new ArgumentNullException(nameof(category));
            }
            try
            {
                _cache.Set(category, cacheKey);
            }
            catch (Exception)
            {

                throw;
            }
            return category;
        }

        public void ClearCache(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }
            try
            {
                _cache.Remove(cacheKey);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
