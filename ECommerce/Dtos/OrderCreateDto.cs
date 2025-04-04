﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class OrderCreateDto
    {

        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ShippingAddressId { get; set; }
        [Required]
        public List<OrderItemCreateDTO> Items { get; set; } = null!;
    }
}
