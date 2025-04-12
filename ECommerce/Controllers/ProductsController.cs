using ECommerce.Models.Filters;
using ECommerce.Models.Models;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ProductsService service, ILogger<ProductsController> logger) : ControllerBase
    {
        private readonly ProductsService _service = service;
        private readonly ILogger<ProductsController> _logger = logger;

        /// <summary>
        /// GET api/products/{id}
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _service.GetProductByIdAsync(id);
                return product == null ?
                    NotFound(new { message = $"Product with the id:{id} could not be found" }) :
                        Ok(product);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// GET api/products/
        /// </summary>
        /// <param>No params</param>
        /// <returns>Products</returns>
        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductsAsync([FromBody] GetProductFilter filter)
        {
            try
            {
                var products = await _service.GetProductsAsync(filter);
                return products is null ?
                    NotFound(new { message = $"There were no products matching the filter found" }) :
                        Ok(products);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GET api/products/query?pageNumber={pageNumber}&pageSize={pageSize}
        /// </summary>
        /// <param>No Params</param>
        /// <returns>Product List</returns>
        [HttpGet("paged/pageSize={pageNumber:int}&pageSize={pageSize:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductsPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var products = await _service.GetProductsPagedAsync(pageNumber, pageSize);
                return products is null ?
                    NotFound(new { message = $"There were no products found" }) :
                        Ok(products);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// POST api/products/
        /// </summary>
        /// <param>No params</param>
        /// <returns>Created Product</returns>
        [HttpPut]
        [Consumes("application/json")]  
        [ProducesResponseType(201)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
        {
            if(product is null)
            {
                return BadRequest(new { message = "Product must not be null" });
            }
            try
            {
                if (ModelState.IsValid)
                {
                     await _service.CreateProductAsync(product);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return CreatedAtAction(nameof(GetProductByIdAsync), new { Id = product.Id }, product);

        }

        /// <summary>
        /// PUT api/products/{id}
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>No Content</returns>
        [HttpPut("{id:int}/price")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProductPriceByIdAsync(int id, [FromBody] decimal price)
        {
            if (price < 0)
            {
                return BadRequest(new { message = "Product price cannot be less than 0" });
            }
            try
            {
                await _service.UpdateProductPriceByIdAsync(id, price);
            }
            catch (Exception ex)
            {

                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                        throw;
                    case DbUpdateException:
                        if (await _service.GetProductByIdAsync(id) == null)
                        {
                            return NotFound(new { message = $"Product with id:{id} could not be found" });
                        }
                        break;

                }
            }
            return NoContent();
        }

        /// <summary>
        /// PUT api/products/{id}
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>No Content</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProductByIdAsync(int id, [FromBody] Product updatedProduct)
        {
            if (!id.Equals(updatedProduct.Id))
            {
                return BadRequest(new { message = $"Route id:{id} and updated product id:{updatedProduct.Id} mismatch" });
            }
            try
            {
                if (ModelState.IsValid)
                    await _service.UpdateProductByIdAsync(id, updatedProduct);
            }
            catch (Exception ex)
            {

                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                        throw;
                    case DbUpdateException:
                        if (await _service.GetProductByIdAsync(id) == null)
                        {
                            return NotFound(new { message = $"Product with id:{id} could not be found" });
                        }
                        break;

                }
            }
            return Ok(updatedProduct);
        }
    }
}
