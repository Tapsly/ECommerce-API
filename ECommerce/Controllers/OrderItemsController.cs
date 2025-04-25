using ECommerce.Models.Models;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController(OrderItemService service, ILogger<OrderItemsController> logger) : ControllerBase
    {
        private readonly OrderItemService _orderItemService = service;
        private readonly ILogger<OrderItemsController> _logger = logger;

        /// <summary>
        /// GET api/orderItems/{orderId}
        /// Check if orderItems exist for a particular existing Order
        /// </summary>
        /// <param name="id">{OrderId}</param>
        /// <returns>True or False value</returns>
        [HttpGet("{orderId:int}/checkOrderItems")]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<bool> CheckOrderItemsByOrderIdAsync(int orderId)
        {
            try
            {
                _logger.LogInformation($"Checking if there are order items in the database for order:{orderId}");
                return await _orderItemService.OrderHasOrderItems(orderId);
            }
            catch (Exception ex)
            {
                var hasOrderItems = await _orderItemService.HasOrderItems();
                if (!hasOrderItems)
                {
                    _logger.LogInformation("There are no order items in the database");
                    return hasOrderItems;
                }
                else
                {
                    _logger.LogError($"This happened tyring to check for order items for order:{orderId}: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// GET api/orderItems/checkOrderItems
        /// Check if there are any orderItems existing in the database
        /// </summary>
        /// <returns>True or False value</returns>
        [HttpGet("/checkOrderItems")]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<bool> CheckOrderItemsAsync()
        {
            try
            {
                _logger.LogInformation("CheckOrderItemsAsync action method hit and running..");
                _logger.LogInformation($"Checking if there are any order items in the database..");
                return await _orderItemService.HasOrderItems();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
        /// <summary>
        /// GET api/orderItems/{orderId}
        /// Fetch all orderItems for a particular existing Order
        /// </summary>
        /// <param name="id">{OrderId}</param>
        /// <returns>A List of Order Items</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetOrderItemsByOrderIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("GetOrderItemsByOrderAsync action method hit and running...");
                var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(id);
                return orderItems == null ?
                    NotFound(new { message = $"No orderItems of the orderId:{id} were found" }) :
                        Ok(orderItems);

            }
            catch (Exception ex)
            {
                if (!(await _orderItemService.HasOrderItems()))
                {
                    _logger.LogInformation("There are no order items in the database");
                    return NotFound(new { message = "There are no order items in storage" });
                }
                else
                {
                    _logger.LogInformation($"{ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// GET api/orderItems/{productId}
        /// Fetch all orderItems for a particular product 
        /// </summary>
        /// <param name="id">{productId}</param>
        /// <returns>A list of order items</returns>
        [HttpGet("{id:int}/orderItems")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetOrderItemsByProductIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("GetOrderItemsByProductIdAsync action method hit and running...");
                var orderItems = await _orderItemService.GetOrderItemsByProductIdAsync(id);
                return orderItems == null ?
                    NotFound(new { message = $"No orderItems of the orderId:{id} were found" }) :
                        Ok(orderItems);

            }
            catch (Exception ex)
            {
                if (!(await _orderItemService.HasOrderItems()))
                {
                    _logger.LogInformation("There are no order items in the database at all");
                    return NotFound(new { message = "There are no order items in storage" });
                }
                else
                {
                    _logger.LogInformation($"{ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// DELETE api/orderItems/{orderId}.
        /// Delete all orderItems for a particular existing Order.
        /// This can be used if and when there is an order that was deleted
        /// and its orderItems needs to be deleted as well.
        /// </summary>
        /// <param name="id">{OrderId}</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id:int}/orderItems")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteOrderItemsByOrderIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("DeleteOrderItemsByOrderAsync action method hit and running...");
                await _orderItemService.DeleteOrderItemsByOrderIdAsync(id);
            }
            catch (Exception ex)
            {
                if (!(await _orderItemService.HasOrderItems()))
                {
                    _logger.LogInformation("There are no order items in the database at all");
                    return NotFound(new { message = "There are no order items in storage" });
                }
                else
                {
                    _logger.LogInformation($"{ex.Message}");
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// DELETE api/orderItems/{orderId}/{orderItemId}.
        /// Delete an orderItem for a particular existing Order.
        /// </summary>
        /// <param name="id">{orderId}</param>
        /// <param name="id">{orderItemId}</param>
        /// <returns>No Content</returns>
        [HttpDelete("{orderId:int}/{orderItemId:int}/orderItem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteOrderItemsByIdAsync(int orderId, int orderItemId)
        {
            try
            {
                _logger.LogInformation("DeleteOrderItemsByIdAsync action method hit and running...");
                await _orderItemService.DeleteOrderItemByIdAsync(orderId, orderItemId);
            }
            catch (Exception ex)
            {
                if (!(await _orderItemService.HasOrderItems()))
                {
                    _logger.LogInformation("There are no order items in the database at all");
                    return NotFound(new { message = "There are no order items in storage" });
                }
                else
                {
                    _logger.LogInformation($"{ex.Message}");
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// UPDATE api/orderItems/{id}.
        /// Delete an orderItem for a particular existing Order.
        /// </summary>
        /// <param name="id">{orderItemId}</param>
        /// <returns>No Content</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        // Prevent over posting on this one
        public async Task<IActionResult> UpdateOrderItemByIdAsync(int id, [FromBody] OrderItem updatedOrderItem)
        {
            _logger.LogInformation("UpdateOrderItemByIdAsync action method hit and running...");
            _logger.LogWarning("UpdatedOrderItem can be null here, please verify");
            if (updatedOrderItem == null)
                return BadRequest(new { message = "updatedOrderItem must not be null" });

            try
            {
                if (ModelState.IsValid)
                {
                    await _orderItemService.UpdateOrderItemByIdAsync(id, updatedOrderItem);
                }
                else
                {
                    return BadRequest(new { message = "Invalid updatedOrderItem found in request" });
                }
            }
            catch (Exception ex)
            {
                if (!(await _orderItemService.HasOrderItems()))
                {
                    _logger.LogInformation("There are no order items in the database at all");
                    return NotFound(new { message = "There are no order items in storage" });
                }
                else
                {
                    _logger.LogInformation($"{ex.Message}");
                    throw;
                }
            }
            return NoContent();
        }
    }
}
