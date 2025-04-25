using ECommerce.Models.Models;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(CustomersService customersService, ILogger<CustomersController> logger) : ControllerBase
    {
        private readonly CustomersService _customersService = customersService;
        private readonly ILogger<CustomersController> _logger = logger;

        /// <summary>
        /// Get the list of registered customers
        /// There is need to be authenticated and have a scope requirement of read:messages
        /// </summary>
        /// <returns>List of registered customers</returns>
        [HttpGet(Name = "customers")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            _logger.LogInformation("GetAllCustomersAsync endpoint hit");
            try
            {
                var customers = await _customersService.GetAllCustomersAsync();
                return customers == null ?
                    StatusCode(4040, NotFound(new { message = "There were no customers found" }))
                        : StatusCode(200, Ok(customers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal servicer error");
            }
        }

        /// <summary>
        /// GET api/customers/
        /// Retrieves a customer by their id. 
        /// </summary>
        /// <returns>A Customer object</returns>
        [HttpGet("{id:int}")]
        [Authorize] 
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            if (int.IsNegative(id))
                return StatusCode(400, BadRequest(new { message = "Id must not be a negative value" }));

            try
            {
                var customer = await _customersService.GetCustomerByIdAsync(id);
                return customer == null ? StatusCode(404, NotFound(new { message = $"No customer with id:{id} was found" }))
                    : StatusCode(200,Ok(customer));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }
        /// <summary>
        /// GET api/customers/
        /// Retrieves a customer by their email. 
        /// The email is extracted from the request body instead of the Route
        /// </summary>
        /// <returns>A Customer object</returns>
        [HttpGet]
        [Consumes("application/json")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomerByEmailAsync([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogError("Email is null or empty here");
                return StatusCode(400, BadRequest(new { message = "Email must not be null" }));
            }
            try
            {
                var customer = await _customersService.GetCustomerByEmailAsync(email);
                return customer == null ?
                    StatusCode(404,NotFound(new { message = $"No customer with email with email:{email}" }))
                        : StatusCode(200,Ok(customer));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, new { message = $"Internal Server Error:{ex.Message}" });
            }
        }
        /// <summary>
        /// GET api/customers
        /// Retrieves a customer by both id and email values. 
        /// Id and Email and extracted from the request body instead of the Route
        /// </summary>
        /// <returns>A customer object</returns>
        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomerByEmailAndPasswordAsync([FromBody] string email, [FromBody] string password)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                _logger.LogError("Email and password are null or empty here");
                return StatusCode(400, BadRequest(new { message = "Email and password must not be null or empty" }));
            }
            try
            {
                var customer = await _customersService.GetCustomerByEmailAndPasswordAsync(email, password);
                return customer == null ?
                    StatusCode(404, NotFound(new { message = $"Customer with email: {email} and password:{password} was not found" }))
                        : StatusCode(200, Ok(customer));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, new { message = $"Internal Server Error:{ex.Message}" });
            }
        }
        /// <summary>
        /// POST api/customers/register
        /// Creates a new customer row. 
        /// This operation requires authentication and 
        /// an update:users scope
        /// </summary>
        /// <returns>CreateAt link with the id and the created customer object</returns>
        [HttpPost(Name = "register")]
        [Consumes("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer customer)
        {
            _logger.LogWarning("customer obj here could be null, no customer should be saved as null");
            if (customer is null)
                return StatusCode(400,BadRequest(new { message = "customer object must not be null" }));

            try
            {
                if (ModelState.IsValid)
                    await _customersService.CreateCustomerAsync(customer);
            }
            catch (Exception ex)
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("The customer object from the client contains invalid fields");
                    return BadRequest(new { messag = "Invalid customer data" });
                }
                else
                {
                    _logger.LogError($"{ex.Message}");
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }

            }
            return StatusCode(201,CreatedAtAction(nameof(GetCustomerByIdAsync), new { id = customer.Id }, customer));

        }

        /// <summary>
        /// UPDATE api/customers/updateCustomer/{id}.
        /// Find a customer by its id and update.
        /// In order for this operation to be performed
        /// the user must be authenticated and with a update:message
        /// scope
        /// </summary>
        /// <param name="id">CustomerId</param>
        /// <returns>No Content</returns>
        [HttpPut("{id:int}", Name = "updateCustomer")]
        [Authorize("update:messages")]
        [Consumes("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCustomerByIdAsync(int id, [FromBody] Customer customer)
        {
            if (int.IsNegative(id))
                return BadRequest(new { message = $"id:{id} must not be a negative value" });

            if (customer is null)
                return BadRequest(new { message = "Attempting to update an existing customer with a null value" });

            if (id != customer.Id)
                return BadRequest(new { message = $"Route id:{id} and customer id:{customer.Id} mismatch" });

            _logger.LogWarning("Customer id might not be the same as the provided route id");
            if (!id.Equals(customer.Id))
            {
                return BadRequest(new { message = $"route id: {id} and customer object Id :{customer.Id} mismatch" });
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _customersService.UpdateCustomerByIdAsync(id, customer);
                }
            }
            catch (Exception ex)
            {
                if (ModelState.IsValid)
                {
                    return BadRequest(new { message = "Customer object contains invalid fields" });
                }
                else
                {
                    _logger.LogError($"{ex.Message}");
                    return StatusCode(500, new { message = $"Internal Server Error:{ex.Message} " });
                }
            }
            return StatusCode(201, NoContent());
        }
        /// <summary>
        /// Find a customer by id and update the membership tier
        /// In order for this operation to be performed
        /// the user must be authenticated and with a update:message
        /// scope
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No Content</returns>
        [HttpPut("{id:int}", Name = "updateMembership")]
        [Authorize("update:messages")]
        [Consumes("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCustomerMembershipTierByIdAsync(int id, [FromBody] MembershipTier membershipTier)
        {
            if (int.IsNegative(id))
                return StatusCode(400, BadRequest(new { message = $"Id must not be a negative value" }));

            if (membershipTier is null)
                return StatusCode(400, BadRequest(new { message = "Attempting to update an existing membershipTier with a null value" }));

            if (!ModelState.IsValid)
            {
                _logger.LogError("Some or all of the membership tier properties are invalid");
                return StatusCode(400, BadRequest(ModelState));
            }

            try
            {
                if (ModelState.IsValid)
                    await _customersService.UpdateCustomerMembershipTier(id, membershipTier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, "Something happened while trying to update the membershipTier");
            }
            return StatusCode(201, NoContent());
        }
        /// <summary>
        /// Delete a single customer by its email.
        /// In order for this operation to be performed
        /// the user must be authenticated and with a delete:message
        /// scope
        /// </summary>
        /// <param name="id">{customer email}</param>
        /// <returns>No Content</returns>
        [HttpDelete("{email:string}")]
        [Authorize("delete:messages")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCustomerByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return StatusCode(400, BadRequest(new { message = "email is null or empty" }));
            try
            {
                await _customersService.DeleteCustomerByEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return StatusCode(201, NoContent());
        }

        /// <summary>
        /// Delete a single customer by its Id.
        /// In order for this operation to be performed
        /// the user must be authenticated and with a delete:message
        /// scope
        /// </summary>
        /// <param name="id">{customerId}</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id:int}")]
        [Authorize("delete:messages")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCustomerByIdAsync(int id)
        {
            if (id < 0)
                return StatusCode(400, BadRequest(new { message = "Id cannot be a negative value" }));
            try
            {
                await _customersService.DeleteCustomerByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
            return StatusCode(201, NoContent());
        }

    }
}
