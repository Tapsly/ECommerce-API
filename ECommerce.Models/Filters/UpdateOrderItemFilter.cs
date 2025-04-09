using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Filters
{
    public class UpdateOrderItemFilter
    {
        public int? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Decimal { get; set; }
    }
}

