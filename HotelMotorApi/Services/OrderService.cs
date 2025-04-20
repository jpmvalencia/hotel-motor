using HotelMotorApi.Interfaces;
using AutoMapper;
using HotelMotorShared.Dtos.OrderDTOs;
using HotelMotorShared.Models;

namespace HotelMotorApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IServiceService _serviceService;
        private readonly IVehiclesService _vehiclesService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository,
            IServiceService serviceService, IVehiclesService vehiclesService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _serviceService = serviceService;
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

        public async Task<OrderDTO> AddServicesToOrder(int orderId, List<int> servicesIds)
        {
            if (!await _orderRepository.ExistsAsync(orderId))
                throw new ApplicationException($"Orden con id {orderId} no encontrada");

            if (servicesIds == null || servicesIds.Count == 0)
                throw new ApplicationException("No se han proporcionado correctamente los ids de los servicios");

            var existingServices = await _serviceService.GetServicesAsync();
            var existingServiceIds = existingServices.Select(s => s.Id).ToList();

            var nonExistingServiceIds = servicesIds.Except(existingServiceIds).ToList();

            if (nonExistingServiceIds.Any())
            {
                throw new ApplicationException($"Los siguientes servicios no existen: {string.Join(", ", nonExistingServiceIds)}");
            }

            var assignedServices = await _orderDetailsRepository.GetServicesByOrderIdAsync(orderId);
            var alreadyAssigned = servicesIds.Intersect(assignedServices.Select(s => s.ServiceId)).ToList();

            if (alreadyAssigned.Any())
                throw new ApplicationException($"Los siguientes servicios ya están asignados a la orden: {string.Join(", ", alreadyAssigned)}");

            await _orderDetailsRepository.AssignServicesToOrderAsync(orderId, servicesIds);

            var updatedOrder = await _orderRepository.GetByIdAsync(orderId);
            return _mapper.Map<OrderDTO>(updatedOrder);
        }

        public async Task<bool> DeleteServiceFromOrder(int orderId, int serviceId)
        {
            return await _orderDetailsRepository.RemoveServiceFromOrderAsync(orderId, serviceId);
        }
    }
}
