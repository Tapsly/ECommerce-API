/*
 * This file contains code that exposes the Web API to the client application
 * The code exposes action methods namely  GET, POST, PUT, PATCH and DELETE
 * Each action methods must implement statusCode api design for proper documentation
 * and should also include the route name with which will be used in the route Url.
 */
using ECommerce.Models.Models;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class CategoriesController(CategoriesService service) : ControllerBase
    {
        private readonly CategoriesService _service = service;
        /// <summary>
        /// route api/categories/categories/ 
        /// Used to fetch a list of categories
        /// </summary>
        /// <param name="id">No parameter</param>
        /// <returns>A list of existing categories</returns>
        [Route("Categories")]
        [HttpGet]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType<Category>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Category>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<Category>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            try
            {
                var categories = await _service.GetCategoriesAsync();
                return categories == null ?
                    NotFound(new { message = "No categories were found" }) : Ok(categories);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GET api/categories/category/{id} 
        /// fetches an existing category
        /// </summary>
        /// <param name="id">{id}</param>
        /// <returns>Category</returns>
        [Route("Category")]
        [HttpGet("{id:int}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType<Category>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Category>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<Category>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _service.GetCategoryByIdAsync(id);
                return category == null ?
                    NotFound(new { message = $"Category with the id: {id} could not be found" }) : Ok(category);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// route api/categories/saveCategory/ 
        /// post request 
        /// </summary>
        /// <param name="id">No params</param>
        /// <returns>Hypertext to the created category, and the category object</returns>
        [Route("SaveCategory")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType<Category>(StatusCodes.Status201Created)]
        [ProducesResponseType<Category>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<Category>(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType<Category>(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            if (category is null)
            {
                return BadRequest(new {message = "Category object cannot be null"});
            }
            try
            {
                await _service.CreateCategoryAsync(category);
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = category.Id }, category);
        }
        /// <summary>
        /// route api/categories/deleteCategory/{id} 
        /// deletes an existing category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>No Content</returns>
        [Route("UpdateCategory")]
        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType<Category>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<Category>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Category>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<Category>(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType<Category>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategoryByIdAsync(int id, [FromBody] Category updatedCategory)
        {
            if (updatedCategory is null)
            {
                return BadRequest(new
                {
                    message = "Category object cannot be null"
                });
            }
            else if (!id.Equals(updatedCategory.Id))
            {
                return BadRequest(new
                {
                    message = $"route id :{id} and category id: {updatedCategory.Id} mismatch"
                });
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateCategoryByIdAsync(id, updatedCategory!);
                }
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                        if (await _service.GetCategoryByIdAsync(id) == null)
                        {
                            return BadRequest(new { message = $"category with the id:{id} does not exist" });
                        }
                        break;
                    case ArgumentNullException:
                        return BadRequest(new { message = "category object cannot be null" });
                    case DbUpdateException:
                        throw;
                }
            }
            return NoContent();
        }
        /// <summary>
        /// route api/categories/updateCategory/{id} 
        /// This is used for patch updates on an existing category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Updated Category Object</returns>
        /*
            Patch method coming soon ......
         */

        /// <summary>
        /// route api/categories/deleteCategory/{id} 
        /// deletes an existing category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>No Content</returns>
        [Route("DeleteCategory")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType<Category>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<Category>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Category>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<Category>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategoryByIdAsync(int id)
        {
            try
            {
                await _service.DeleteCategoryByIdAsync(id);
            }
            catch (Exception)
            {
                if (await _service.GetCategoryByIdAsync(id) == null)
                {
                    return BadRequest(new { message = $"Category with the id: {id} does not exist" });
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        /// <summary>
        /// route api/categories/deleteCategories/
        /// DELETE request to delete all existing categories
        /// </summary>
        /// <param>No params</param>
        /// <returns>No Content</returns>
        [Route("DeleteCategories")]
        [HttpDelete]
        [ProducesResponseType<Category>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<Category>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Category>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<Category>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategoriesAsync()
        {
            try
            {
                await _service.DeleteCategoriesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }
    }
}
