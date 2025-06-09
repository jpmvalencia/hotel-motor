using AutoMapper;
using HotelMotorApi.Interfaces;
using HotelMotorApi.Modules.EmailSender.Interfaces;
using HotelMotorShared.Dtos.OrderDTOs;
using HotelMotorShared.DTOs.ServiceDTOs;
using HotelMotorShared.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace HotelMotorApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IServiceService _serviceService;
        private readonly IVehiclesService _vehiclesService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository,
            IServiceService serviceService, IVehiclesService vehiclesService, 
            IEmailSenderService emailSenderService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _serviceService = serviceService;
            _vehiclesService = vehiclesService;
            _emailSenderService = emailSenderService;
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
                DueDate = orderDto.DueDate,
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
                throw new Exception("No se proporcionaron los datos correctamente");

            if (orderDto.Summary != null)
            {
                existingOrder.Summary = orderDto.Summary;
            }
            if (orderDto.Status.HasValue)
            {
                existingOrder.Status = orderDto.Status.Value;
            }
            if (orderDto.Status.HasValue && orderDto.Status.Value == OrderStatus.Completed)
            {
                var customer = await _vehiclesService.GetCustomerByVehicleIdAsync(orderDto.VehicleId);
                await _emailSenderService.SendEmailAsync
                    (
                        customer?.Email,
                        "Orden Completada",
                        $"Su orden con ID {existingOrder.Id} ha sido completada. Gracias por su preferencia."
                    );
            }
            existingOrder.DueDate = orderDto.DueDate;
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

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order != null)
                await UpdateOrderTotalAmountAsync(order);

            var updatedOrder = await _orderRepository.GetByIdAsync(orderId);
            return _mapper.Map<OrderDTO>(updatedOrder);
        }

        public async Task<bool> DeleteServiceFromOrder(int orderId, int serviceId)
        {
            var serviceToRemove = await _serviceService.GetServiceByIdAsync(serviceId);
            if (serviceToRemove == null)
            {
                throw new ApplicationException($"El servicio con ID {serviceId} no existe.");
            }

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ApplicationException($"La orden con ID {orderId} no existe.");
            }
            var isServiceAssigned = await _orderDetailsRepository.IsServiceAssignedToOrderAsync(orderId, serviceId);
            if (!isServiceAssigned)
            {
                throw new ApplicationException($"El servicio con ID {serviceId} no está asignado a la orden con ID {orderId}.");
            }

            var result = await _orderDetailsRepository.RemoveServiceFromOrderAsync(orderId, serviceId);

            if (result)
            {
                order.TotalAmount -= serviceToRemove.Price;
                await _orderRepository.UpdateAsync(order);
            }

            return result;
        }

        public async Task<OrderDTO> UpdateOrderTotalAmountAsync(Order order)
        {
            var orderDetails = await _orderDetailsRepository.GetServicesByOrderIdAsync(order.Id);
            var totalAmount = orderDetails.Sum(od => od.Service.Price);
            order.TotalAmount = totalAmount;
            await _orderRepository.UpdateAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }
    }
}
