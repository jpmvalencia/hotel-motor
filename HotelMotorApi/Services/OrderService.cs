using HotelMotorApi.Interfaces;
using AutoMapper;
using HotelMotorShared.Dtos.OrderDTOs;
using HotelMotorShared.Models;

namespace HotelMotorApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IVehiclesRepository vehiclesRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _vehiclesRepository = vehiclesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return null;
            return _mapper.Map<OrderDTO>(order);
        }
        public async Task<OrderDTO> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var vehicle = await _vehiclesRepository.GetVehicleByIdAsync(orderDto.VehicleId) ?? 
                throw new Exception("Vehículo no encontrado");

            var order = new Order
            {
                Summary = orderDto.Summary,
                Vehicle = vehicle,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                TotalAmount = 0.0m,
                Status = "PENDING",
            };
            order = await _orderRepository.CreateOrderAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }
    }
}
