using ECommerce.Models.Models;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingDetailsController(TrackingDetailsService service) : ControllerBase
    {
        private readonly TrackingDetailsService _service = service;
        /// <summary>
        /// api/trackingDetails/{orderId}
        /// </summary>
        /// <param name="orderId">{OrderId}</param>
        /// <returns>TrackingDetails of the specified order by Id</returns>
        [HttpGet("{orderId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetTrackingDetailsByOrderIdAsync(int orderId)
        {
            try
            {
                var trackingDetail = await _service.GetTrackingDetailsByOrderIdAsync(orderId);
                return trackingDetail == null ?
                    NotFound(new
                    {
                        message = $"TrackingDetails with the id:{orderId} could not be found"
                    }) : Ok(trackingDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// api/trackingDetails/{trackingNumber}
        /// </summary>
        /// <param name="orderId">{trackingNumber}</param>
        /// <returns>TrackingDetails of the specified order by trackingNumber</returns>
        [HttpGet("{trackingNumber: string}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetTrackingDetailsByTrackingNumberAsync(string trackingNumber)
        {
            try
            {
                var trackingDetail = await _service.GetTrackingDetailsByTrackingNumberAsync(trackingNumber);
                return trackingDetail == null ?
                    NotFound(new
                    {
                        message = $"TrackingDetails with the id:{trackingNumber} could not be found"
                    }) : Ok(trackingDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// api/trackingDetails/{trackingNumber}
        /// </summary>
        /// <param name="orderId">{trackingNumber}</param>
        /// <returns>TrackingDetails of the specified order by trackingNumber</returns>
        [HttpPut("{orderId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<TrackingDetail>> UpdateTrackingDetailByOrderIdAsync(int orderId, [FromBody] TrackingDetail updatedTrackingDetail)
        {
            if (updatedTrackingDetail is null)
            {
                return BadRequest(new { message = "Tracking details must not be null" });
            }
            else if (!orderId.Equals(updatedTrackingDetail.OrderId))
            {
                return BadRequest(new { message = $"orderId:{orderId} and updatedTrackingDetail id:{updatedTrackingDetail!.OrderId} mismatch" });
            }
            try
            {
                if (ModelState.IsValid)
                    await _service.UpdateTrackingDetailByOrderIdAsync(orderId, updatedTrackingDetail);
            }
            catch (Exception ex)
            {
                if (ModelState.ErrorCount > 0)
                {
                    return BadRequest(new { message = "TrackingDetails validation failed" });
                }
                switch (ex)
                {
                    case DbUpdateException:
                        if (await _service.GetTrackingDetailsByOrderIdAsync(orderId) == null)
                        {
                            return NotFound(new { message = $"TrackingDetails with the id:{orderId} could not be found" });
                        }
                        break;
                    case DBConcurrencyException:
                        throw;
                }
            }
            return updatedTrackingDetail;

        }

        /// <summary>
        /// api/trackingDetails/{trackingNumber}
        /// </summary>
        /// <param name="orderId">{trackingNumber}</param>
        /// <returns>TrackingDetails of the specified order by trackingNumber</returns>
        [HttpPut("{trackingNumber:string}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<TrackingDetail>> UpdateTrackingDetailByTrackingNumberAsync(string trackingNumber, [FromBody] TrackingDetail updatedTrackingDetail)
        {
            if (updatedTrackingDetail is null)
            {
                return BadRequest(new { message = "Tracking details must not be null" });
            }
            if (string.IsNullOrEmpty(trackingNumber))
            {
                return BadRequest(new { message = "trackingNumber must be be null or an empty string" });
            }
            else if (!trackingNumber.Equals(updatedTrackingDetail.TrackingNumber))
            {
                return BadRequest(new { message = $"orderId:{trackingNumber} and updatedTrackingDetail id:{updatedTrackingDetail!.TrackingNumber} mismatch" });
            }
            try
            {
                if (ModelState.IsValid)
                    await _service.UpdateTrackingDetailByTrackingNumberAsync(trackingNumber, updatedTrackingDetail);
            }
            catch (Exception ex)
            {
                if (ModelState.ErrorCount > 0)
                {
                    return BadRequest(new { message = "TrackingDetails validation failed" });
                }
                switch (ex)
                {
                    case DbUpdateException:
                        if (await _service.GetTrackingDetailsByTrackingNumberAsync(trackingNumber) == null)
                        {
                            return NotFound(new { message = $"TrackingDetails with the id:{trackingNumber} could not be found" });
                        }
                        break;
                    case DBConcurrencyException:
                        throw;
                }
            }
            return updatedTrackingDetail;
        }


        /// <summary>
        /// api/trackingDetails/{trackingNumber}
        /// </summary>
        /// <param name="trackingNumber">{trackingNumber}</param>
        /// <returns>No Content</returns>
        [HttpDelete("{trackingNumber: string}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteTrackingDetailsByTrackingNumberAsync(string trackingNumber)
        {
            try
            {
                await _service.DeleteTrackingDetailByTrackingNumberAsync(trackingNumber);
            }
            catch (Exception)
            {
                if(await _service.GetTrackingDetailsByTrackingNumberAsync(trackingNumber) == null)
                {
                    return BadRequest(new { message = $"TrackingDetails with the tracking number :{trackingNumber} could not be found" });
                }
                else
                {
                    throw;

                }
            }
            return NoContent();
        }
        /// <summary>
        /// api/trackingDetails/{orderId}
        /// </summary>
        /// <param name="orderId">{orderId}</param>
        /// <returns>No Content</returns>
        [HttpDelete("{orderId: int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteTrackingDetailByOrderIdAsync(int orderId)
        {
            try
            {
                await _service.DeleteTrackingDetailByOrderIdAsync(orderId);
            }
            catch (Exception)
            {
                if (await _service.GetTrackingDetailsByOrderIdAsync(orderId) == null)
                {
                    return BadRequest(new { message = $"TrackingDetails with the order Id :{orderId} could not be found" });
                }
                else
                {
                    throw;

                }
            }
            return NoContent();
        }

        /// <summary>
        /// api/trackingDetails/
        /// Removes all the tracking details of the existing orders
        /// </summary>
        /// <param>No params</param>
        /// <returns>No Content</returns>
        [HttpDelete("removeAll")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(403)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteTrackingDetailsAsync()
        {
            try
            {
                await _service.DeleteTrackingDetailsAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }

    }
}
