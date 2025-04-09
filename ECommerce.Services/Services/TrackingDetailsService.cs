using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services
{
    public class TrackingDetailsService(ECommerceDbContext dbContext) : ITrackingDetail
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        public async Task DeleteTrackingDetailByOrderIdAsync(int orderId)
        {
            var existingTrackingDetail = _dbContext.TrackingDetails.Find(orderId);
            try
            {
                if (existingTrackingDetail != null)
                {
                    _dbContext.TrackingDetails.Remove(existingTrackingDetail);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task DeleteTrackingDetailByTrackingNumberAsync(string trackingNumber)
        {
            var existingTrackingDetail = _dbContext.TrackingDetails.FirstOrDefault(td => td.TrackingNumber == trackingNumber);
            try
            {
                if (existingTrackingDetail != null)
                {
                    _dbContext.TrackingDetails.Remove(existingTrackingDetail);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task DeleteTrackingDetailsAsync()
        {
            try
            {
                _dbContext.TrackingDetails.Where(_ => true).ExecuteDelete();
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TrackingDetail?> GetTrackingDetailsByOrderIdAsync(int orderId)
        {
            try
            {
                return await _dbContext.TrackingDetails.AsNoTracking().FirstOrDefaultAsync(td => td.OrderId == orderId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TrackingDetail?> GetTrackingDetailsByTrackingNumberAsync(string trackingNumber)
        {
            try
            {
                return await _dbContext.TrackingDetails.AsNoTracking().FirstOrDefaultAsync(td => td.TrackingNumber == trackingNumber);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TrackingDetail> UpdateTrackingDetailByOrderIdAsync(int orderId, TrackingDetail updatedTrackingDetail)
        {
            var existingTrackingDetail = _dbContext.TrackingDetails.FirstOrDefault(td => td.OrderId == orderId);
            try
            {
                if (existingTrackingDetail != null)
                {
                    _dbContext.TrackingDetails.Entry(existingTrackingDetail).CurrentValues.SetValues(updatedTrackingDetail);
                    await _dbContext.SaveChangesAsync();
                }
                return updatedTrackingDetail;
            }
            catch (DbUpdateException)
            {

                throw;
            }
        }

        public async Task<TrackingDetail> UpdateTrackingDetailByTrackingNumberAsync(string trackingNumber, TrackingDetail updatedTrackingDetail)
        {
            var existingTrackingDetail = _dbContext.TrackingDetails.FirstOrDefault(td => td.TrackingNumber == trackingNumber);
            try
            {
                if (existingTrackingDetail != null)
                {
                    _dbContext.TrackingDetails.Entry(existingTrackingDetail).CurrentValues.SetValues(updatedTrackingDetail);
                    await _dbContext.SaveChangesAsync();
                }
                return updatedTrackingDetail;
            }
            catch (DbUpdateException)
            {

                throw;
            }
        }
    }
}
