using HotelMotorApi.Interfaces;
using AutoMapper;
using HotelMotorShared.Dtos.OrderDTOs;
using HotelMotorShared.Models;

namespace HotelMotorApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVehiclesService _vehiclesService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IVehiclesService vehiclesService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _vehiclesService = vehiclesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return null;
            return _mapper.Map<OrderDTO>(order);
        }
        public async Task<OrderDTO> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var vehicle = await _vehiclesService.GetVehicleByIdAsync(orderDto.VehicleId) ??
                throw new Exception("Vehículo no encontrado");

            var order = new Order
            {
                Summary = orderDto.Summary,
                Vehicle = vehicle,
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(orderDto.DueDateDays),
                TotalAmount = 0.0m,
                Status = OrderStatus.Pending
            };
            order = await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO?> UpdateOrderAsync(int id, OrderUpdateDTO orderDto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                return null;

            if (orderDto.Summary != null)
                existingOrder.Summary = orderDto.Summary;

            if (orderDto.Status.HasValue)
                existingOrder.Status = orderDto.Status.Value;

            if (orderDto.DueDateDays.HasValue)
                existingOrder.DueDate = DateTime.Now.AddDays(orderDto.DueDateDays.Value);

            await _orderRepository.UpdateAsync(existingOrder);
            return _mapper.Map<OrderDTO>(existingOrder);
        }


        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
