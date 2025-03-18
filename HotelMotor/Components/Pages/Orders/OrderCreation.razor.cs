using HotelMotor.Modules.OrderDetails;
using HotelMotor.Modules.OrderDetails.Models;
using HotelMotor.Modules.Orders;
using HotelMotor.Modules.Orders.Models;
using Microsoft.AspNetCore.Components;

namespace HotelMotor.Components.Pages.Orders
{
    public partial class OrderCreation
    {
        [Inject]
        private OrderService OrderService { get; set; }

        [Inject]
        private OrderDetailService OrderDetailService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<OrderDetail> _orderDetails = new List<OrderDetail>();

        private Order newOrder = new Order();

        private void HandleValidSubmit()
        {
            OrderService.AddOrder(newOrder);
            NavigationManager.NavigateTo("/");
        }

        private void AddOrderDetail()
        {
            var newDetail = new OrderDetail
            {
                Id = _orderDetails.Count + 1,
                OrderId = newOrder.Id,
                ServiceId = 0,
                Service = null,
            };
            _orderDetails.Add(newDetail);
        }

        protected void GetOrderDetails()
        {
            _orderDetails = OrderDetailService.GetOrderDetails();
        }

        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var detail in _orderDetails)
            {
                if (detail.Service != null)
                    total += detail.Service.Price;
            }
            return total;
        }
    }
}