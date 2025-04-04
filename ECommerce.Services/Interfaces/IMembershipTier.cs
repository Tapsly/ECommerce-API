using ECommerce.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface IMembershipTier
    {
        public Task<List<MembershipTier>> GetMembershipTiersAsync();
        public Task<MembershipTier?> GetMembershipTierByIdAsync(int id);
        public Task UpdateMembershipTierByIdAsync(int id, MembershipTier updatedMembershipTier);
        public Task DeleteMembershipTiersAsync();
        public Task DeleteMembershipTierByIdAsync(int id);
    }
}
