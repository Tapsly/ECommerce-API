using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services
{
    public class MembershipTiersService(ECommerceDbContext dbContext) : IMembershipTier
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        public async Task DeleteMembershipTierByIdAsync(int id)
        {
            var existingMembership = _dbContext.MembershipTiers.Find(id);
            try
            {
                _dbContext.MembershipTiers.Remove(existingMembership!);
                // update all users with this membershipTier
                /*await _dbContext.Customers.Where(c => c.MembershipTier!.Id == id).ExecuteUpdateAsync(
                    setters => setters.SetProperty(c => c.MembershipTier, null));*/
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteMembershipTiersAsync()
        {
            try
            {
                await _dbContext.MembershipTiers.ExecuteDeleteAsync();
                // update all users with membershipTiers
                /*await _dbContext.Customers.ExecuteUpdateAsync(setters => setters 
                    .SetProperty(c => c.MembershipTier, null));
                */
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MembershipTier?> GetMembershipTierByIdAsync(int id)
        {
            try
            {
                return await _dbContext.MembershipTiers.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<MembershipTier>> GetMembershipTiersAsync()
        {
            try
            {
                return await _dbContext.MembershipTiers.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task UpdateMembershipTierByIdAsync(int id, MembershipTier updatedMembershipTier)
        {
            throw new NotImplementedException();
        }
    }
}
