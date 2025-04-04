using ECommerce.Dtos;
using ECommerce.Models.Models;

namespace ECommerce.MappingProfiles
{
    public class TrackingDetailsMappingProfiles
    {
        public static TrackingDetail MapToTrackingDetail(TrackingDetailCreateDto trackingDetailsCreateDTO)
        {
            return new TrackingDetail
            {
                OrderId = trackingDetailsCreateDTO.OrderId,
                Carrier = trackingDetailsCreateDTO.Carrier,
                EstimatedDeliveryDate = trackingDetailsCreateDTO.EstimatedDeliveryDate,
                TrackingNumber = trackingDetailsCreateDTO.TrackingNumber,
            };
        }

        public static TrackingDetailDto MapToTrackingDetailsDTO(TrackingDetail trackingDetail)
        {
            return new TrackingDetailDto
            {
                Id = trackingDetail.Id,
                OrderId = trackingDetail.OrderId,
                TrackingNumber = trackingDetail.TrackingNumber,
                Carrier = trackingDetail.Carrier,
                EstimatedDeliveryDate = trackingDetail.EstimatedDeliveryDate,
            };
        }

        public static TrackingDetailSummaryDto MapToTrackingDetailsSummaryDTO(TrackingDetail trackingDetail)
        {
            return new TrackingDetailSummaryDto
            {
                TrackingNumber = trackingDetail.TrackingNumber,
                Carrier = trackingDetail.Carrier,
                EstimatedDeliveryDate = trackingDetail.EstimatedDeliveryDate,
                Order = trackingDetail.Order,
            };
        }
    }
}
