using ECommerce.Models.Models;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController(AddressesService service, ILogger<AddressesController> logger) : ControllerBase
    {
        private readonly AddressesService _service = service;
        private readonly ILogger<AddressesController> _logger = logger;

        /// <summary>
        /// api/addresses/{customerId}
        /// </summary>
        /// <param name="id">{customerId}</param>
        /// <returns>Address List</returns>
        [HttpGet("{customerId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAddressesByCustomerIdAsync(int customerId)
        {
            try
            {
                var addresses = await _service.GetAddressesByCustomerIdAsync(customerId);
                return addresses == null ?
                    NotFound(new { message = $"There were no addresses found for customer with id : {customerId}" }) :
                        Ok(addresses);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GET api/addresses/{customerId}/{addressId}
        /// </summary>
        /// <param name="customerId">{customerId}</param>
        /// <param name="addressId">{addressId</param>
        /// <returns>Address</returns>
        [HttpGet("{customerId:int}/{addressId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult>? GetAddressByCustomerIdAndAddressId(int customerId, int addressId)
        {
            try
            {
                var address = await _service.GetAddressByCustomerIdAndAddressIdAsync(customerId, addressId);
                return address == null ?
                    NotFound(new { message = $"The address with id:{addressId} for customer with id : {customerId} could not be found" }) :
                        Ok(address);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// PUT api/addresses/{id}
        /// </summary>
        /// <param name="customerId">{customer id}</param>
        /// <param name="addressId">{Address id}</param>
        /// <returns>Address</returns>
        [HttpPut("{customerId:int}/{addressId:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAddressByCustomerIdAndAddressId(int customerId, int addressId, Address updatedAddress)
        {
            // verify that the route id is the same as the address id that needs to be updated
            if (addressId.Equals(updatedAddress.Id))
            {
                return BadRequest(new { message = $"address Id:{addressId} and updated address Id: {updatedAddress.Id} mismatch" });
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateAddressByCustomerIdAndAddressIdAsync(customerId, addressId, updatedAddress);
                }
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                        throw;
                    case DbUpdateException:
                        if (await _service.GetAddressByCustomerIdAndAddressIdAsync(customerId, addressId) is null)
                        {
                            return NotFound(new { message = $"Address with id: {addressId} for customer with the id:{customerId} could not be found" });
                        }
                        break;
                }
            }
            return Ok(updatedAddress);
        }

        /// <summary>
        /// DELETE api/addresses/{customerId}/{addressId}
        /// </summary>
        /// <param name="customerId">{customerId}</param>
        /// <param name="addressId">{addressId</param>
        /// <returns>No Content</returns>
        [HttpDelete("{customerId:int}/{addressId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAddressByCustomerIdAndAddressId(int customerId, int addressId)
        {
            try
            {
                await _service.DeleteAddressByCustomerAndAddressIdAsync(customerId, addressId);
            }
            catch (Exception)
            {
                if(await _service.GetAddressByCustomerIdAndAddressIdAsync(customerId, addressId) == null)
                {
                    return NotFound(new { mesage = $"Address with the id:{addressId} for the customer with id:{customerId} not found" });
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// api/addresses/{id}
        /// </summary>
        /// <param name="id">{Address id}</param>
        /// <returns>Address</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAddressesByCustomerIdAsync(int customerId)
        {
            try
            {
                await _service.DeleteAddressesByCustomerIdAsync(customerId);
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }

    }
}
