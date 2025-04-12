using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipTiersController(MembershipTiersService service) : ControllerBase
    {
        private readonly MembershipTiersService _service = service;
        /// <summary>
        /// GET api/membershipTiers/{id}
        /// </summary>
        /// <param name="id">{membershipTier id}</param>
        /// <returns>MembershipTier json object</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetMembershipTierByIdAsync(int id)
        {
            try
            {
                var membershipTier = await _service.GetMembershipTierByIdAsync(id);
                return membershipTier == null ?
                    NotFound(new { message = $"Membership tier with the id:{id} does not exist" }) :
                        Ok(membershipTier);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GET api/membershipTiers/
        /// </summary>
        /// <param name="id">{No params}</param>
        /// <returns>MembershipTiers list</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetMembershipTiers()
        {
            try
            {
                var tierList = await _service.GetMembershipTiersAsync();
                return tierList == null ?
                    NotFound(new { message = "There were membership tiers found" }) :
                        Ok(tierList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// DELETE api/membershipTiers/{id}
        /// deletes the existing tier if it exists
        /// </summary>
        /// <param name="id">{membershipTier id}</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteMembershipTierByIdAsync(int id)
        {
            try
            {
                await _service.DeleteMembershipTierByIdAsync(id);
            }
            catch (Exception)
            {
                if (await _service.GetMembershipTierByIdAsync(id) == null)
                {
                    return NotFound(new { message = $"Membership Tier with the id : {id} does not exist" });
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// DELETE api/membershipTiers/
        /// </summary>
        /// <param name="id">{No params}</param>
        /// <returns>MembershipTiers list</returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteMembershipTiers()
        {
            try
            {
                await _service.DeleteMembershipTiersAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }
    }
}
