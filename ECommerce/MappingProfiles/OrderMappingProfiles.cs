using AutoMapper;
using ECommerce.Dtos;
using ECommerce.Models.Models;

namespace ECommerce.MappingProfiles
{
    public class OrderMappingProfiles : Profile
    {
        public OrderMappingProfiles()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber));

            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
                .ForMember(dest => dest.ShippingAddress,
                        opt => opt.MapFrom(src => src.ShippedDate.HasValue ?
                            src.ShippedDate.Value.ToString("YYYY-MM-dd HH:mm:ss") : null));
            // Note: We do NOT explicitly map .ForMember(dest => dest.TrackingDetail, ...)
            // or .ForMember(dest => dest.OrderItems, ...) here because:
            //  - The property names are the same in source and destination
            //  - We have separate mappings for TrackingDetail -> TrackingDetailDTO
            //    and OrderItem -> OrderItemDTO (see below)
            // AutoMapper will automatically use those mappings, as long as
            // the source and destination property names and types align.

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            // We do NOT specify mappings for ProductPrice or TotalPrice 
            // because the property names match exactly in both the source 
            // and the destination (and there's no special logic needed):
            //   Source: OrderItem.ProductPrice  -> Destination: OrderItemDTO.ProductPrice
            //   Source: OrderItem.TotalPrice    -> Destination: OrderItemDTO.TotalPrice
            // AutoMapper will do these by convention.
            //-----------------------------------------------------------------
            // 3. Address -> AddressDTO
            //-----------------------------------------------------------------
            // All property names match, and no special transform is needed,
            // so we don't need ForMember. This single CreateMap is enough.
            CreateMap<Address, AddressDetailsDto>();
            //-----------------------------------------------------------------
            // 4. TrackingDetail -> TrackingDetailDTO
            //-----------------------------------------------------------------
            CreateMap<TrackingDetail, TrackingDetailDto>()
                // We apply a null substitute for TrackingNumber if null.
                .ForMember(dest => dest.TrackingNumber,
                           opt => opt.NullSubstitute("Tracking not available"));
            //-----------------------------------------------------------------
            // 5. OrderCreateDTO -> Order
            //-----------------------------------------------------------------
            CreateMap<OrderCreateDto, Order>()
                // Set the OrderDate to the current time when creating a new order
                .ForMember(dest => dest.OrderDate,
                           opt => opt.MapFrom(src => DateTime.Now))
                // Initialize a default status (e.g., "Pending")
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => "Pending"))
                // Map "OrderCreateDTO.OrderItems" -> "Order.Items" (different property names)
                .ForMember(dest => dest.OrderItems,
                           opt => opt.MapFrom(src => src.Items));
            // We do NOT specify .ForMember for "Amount", "OrderDiscount", or "TotalAmount"
            // because they might be calculated logic in the controller/service layer
            // rather than mapped directly from the DTO.
            //-----------------------------------------------------------------
            // 6. OrderItemCreateDTO -> OrderItem
            //-----------------------------------------------------------------
            // Because the property names match ("ProductId", "Quantity") and
            // there's no special transformation, a default CreateMap is sufficient.
            CreateMap<OrderItemCreateDto, OrderItem>();
        }

    }
}
