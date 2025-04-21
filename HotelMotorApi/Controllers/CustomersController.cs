using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelMotorApi.Common;
using HotelMotorApi.Interfaces;
using HotelMotorShared.Dtos.CustomerDTOs;
using FluentValidation;

namespace HotelMotorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerCreateDto> _createValidator;
        private readonly IValidator<CustomerUpdateDto> _updateValidator;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ICustomerService customerService,
            IValidator<CustomerCreateDto> createValidator,
            IValidator<CustomerUpdateDto> updateValidator,
            ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        // GET: api/Customers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(new
            {
                status = 200,
                message = "Clientes encontrados",
                data = customers
            });
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Cliente no encontrado"
                });
            }

            return Ok(new
            {
                status = 200,
                message = "Cliente encontrado",
                data = customer
            });
        }

        // POST: api/Customers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto customerDto)
        {
            var validationResult = await _createValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Error de validación",
                    errors = validationResult.ToDictionary()
                });
            }

            try
            {
                var createdCustomer = await _customerService.CreateAsync(customerDto);
                return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, new
                {
                    status = 201,
                    message = "Cliente creado exitosamente",
                    data = createdCustomer
                });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Error al crear cliente");
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al crear el cliente",
                    error = ex.Message
                });
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Error de validación",
                    errors = validationResult.ToDictionary()
                });
            }

            try
            {
                var updatedCustomer = await _customerService.UpdateAsync(id, customerDto);
                if (updatedCustomer == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Cliente no encontrado"
                    });
                }

                return Ok(new
                {
                    status = 200,
                    message = "Cliente actualizado exitosamente",
                    data = updatedCustomer
                });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Error al actualizar cliente");
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al actualizar el cliente",
                    error = ex.Message
                });
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Cliente no encontrado"
                });
            }

            return Ok(new
            {
                status = 200,
                message = "Cliente eliminado exitosamente"
            });
        }

        // GET: api/Customers/search?term=nombre_o_email
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> SearchCustomer([FromQuery] string term)
        {
            var customer = await _customerService.SearchOneAsync(term);
            if (customer == null)
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Cliente no encontrado"
                });
            }

            return Ok(new
            {
                status = 200,
                message = "Cliente encontrado",
                data = customer
            });
        }
    }
}