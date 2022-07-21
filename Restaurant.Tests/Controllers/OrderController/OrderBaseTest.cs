using Bogus;
using Moq;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System.Collections.Generic;

namespace Restaurant.Tests.Controllers.OrderController
{
    public class OrderBaseTest
    {
        protected readonly Mock<IOrderService> _orderService;
        protected readonly Mock<IWaiterService> _waiterService;
        protected readonly Mock<IBillService> _billService;
        protected readonly Faker _faker;
        protected readonly List<OrderItems> _orderItems;

        public OrderBaseTest()
        {
            _orderService = new Mock<IOrderService>();
            _waiterService = new Mock<IWaiterService>();
            _billService = new Mock<IBillService>();
            _faker = new();
            _orderItems = new Mock<List<OrderItems>>().Object;
        }
    }
}
