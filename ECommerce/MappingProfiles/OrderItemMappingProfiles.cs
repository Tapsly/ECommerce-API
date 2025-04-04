using ECommerce.Dtos;
using ECommerce.Models.Models;

namespace ECommerce.MappingProfiles
{
    public class OrderItemMappingProfiles
    {
        public static OrderItem MapToOrderItem(OrderItemCreateDto orderItemCreateDTO)
        {
            return new OrderItem
            {
                OrderId = orderItemCreateDTO.OrderId,
                ProductId = orderItemCreateDTO.ProductId,
                Quantity = orderItemCreateDTO.Quantity,
                ProductPrice = orderItemCreateDTO.ProductPrice,
            };
        }

        public static OrderItemDto MapToOrderItemDTO(OrderItem orderItem)
        {
            return new OrderItemDto
            {
                ProductName = orderItem.Product.Name,
                ProductPrice = orderItem.ProductPrice,
                Quantity = orderItem.Quantity,
                Discount = orderItem.Discount,

            };
        }

    }
}
