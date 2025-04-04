using ECommerce.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface ITrackingDetail
    {
        public Task<TrackingDetail?> GetTrackingDetailsByTrackingNumberAsync(string trackingNumber);
        public Task<TrackingDetail?> GetTrackingDetailsByOrderIdAsync(int orderId);
        public Task<TrackingDetail> UpdateTrackingDetailByOrderIdAsync(int orderId, TrackingDetail updatedTrackingDetail);
        public Task<TrackingDetail> UpdateTrackingDetailByTrackingNumberAsync(string trackingNumber, TrackingDetail updatedTrackingDetail);
        public Task DeleteTrackingDetailsAsync();
        public Task DeleteTrackingDetailByOrderIdAsync(int orderId);
        public Task DeleteTrackingDetailByTrackingNumberAsync(string trackingNumber);
    }
}
