using HotelMotorApi.Interfaces;
using HotelMotorApi.Services;
using HotelMotorShared.Dtos.OrderDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelMotorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(new
            {
                status = 200,
                message = "Órdenes encontradas",
                data = orders
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) {
                return NotFound(new
                {
                    status = 404,
                    message = "Orden no encontrada"
                });
            }
            return Ok(new
            {
                status = 200,
                message = "Orden encontrada",
                data = order
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderDto)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(orderDto);
                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, new
                {
                    status = 201,
                    message = "Orden creada",
                    data = createdOrder
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al crear la orden",
                    data = ex.Message
                });
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateDTO orderDto)
        {
            
            try
            {
                var updatedOrder = await _orderService.UpdateOrderAsync(id, orderDto);
                if (updatedOrder == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Orden no encontrado"
                    });
                }

                return Ok(new
                {
                    status = 200,
                    message = "Orden actualizada exitosamente",
                    data = updatedOrder
                });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Error al actualizar orden");
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al actualizar el orden",
                    error = ex.Message
                });
            }
            catch(Exception ex) 
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al actualizar la orden",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var deleted = await _orderService.DeleteOrderAsync(id);
                if (!deleted)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Orden no encontrada"
                    });
                }

                return Ok(new
                {
                    status = 200,
                    message = "Orden eliminada exitosamente"
                });
            } catch (Exception ex)
            {
                return BadRequest(new { status = 400, message = ex.Message });
            }
        }

        [HttpPost("{orderId}/assign-services")]
        public async Task<ActionResult<OrderDTO>> AddServicesToOrder(int orderId, [FromBody] List<int> servicesIds)
        {
            try
            {
                var result = await _orderService.AddServicesToOrder(orderId, servicesIds);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al asignar los servicios en la orden " + ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }

        [HttpDelete("{orderId}/delete-service")]
        public async Task<ActionResult> DeleteServiceFromOrder(int orderId, [FromBody] int serviceId)
        {
            try
            {
                var deleted = await _orderService.DeleteServiceFromOrder(orderId, serviceId);
                if (!deleted)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "La orden no tiene el servicio " + serviceId
                    });
                }
                return Ok(new
                {
                    status = 200,
                    message = "Se eliminó el servicio " + serviceId + " de la orden"
                });
            } catch (Exception ex)
            {
                return BadRequest(new { status = 400, message = ex.Message });
            }
        }
    }
}
