using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System;
using Xunit;

namespace Restaurant.Tests.Controllers.OrderController
{
    public class PostOrderTest : OrderBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenWaiterDoestnExists()
        {
            _ = _waiterService.Setup(_ => _.Get(It.IsAny<Waiter>())).Returns((Waiter)null);

            OrdersController ordersController = new(_orderService.Object, _waiterService.Object, _billService.Object);

            int waiterId = _faker.Random.Int();
            ActionResult result = ordersController.PostOrder(_orderItems, waiterId, _faker.Random.Int()).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);
            
            Assert.Equal($"No one waiter with id {waiterId} was found", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenTableDoesntHaveBillOpen()
        {
            _ = _waiterService.Setup(_ => _.Get(It.IsAny<Waiter>())).Returns(new Waiter());
            _ = _billService.Setup(_ => _.GetByTableNumber(It.IsAny<Bill>())).Returns((Bill)null);

            int tableNumber = _faker.Random.Int();
            OrdersController ordersController = new(_orderService.Object, _waiterService.Object, _billService.Object);

            ActionResult result = ordersController.PostOrder(_orderItems, _faker.Random.Int(), tableNumber).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"No one openned bill to table {tableNumber} was found", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenInsertOrderReturnException()
        {
            _ = _waiterService.Setup(_ => _.Get(It.IsAny<Waiter>())).Returns(new Waiter());
            _ = _billService.Setup(_ => _.GetByTableNumber(It.IsAny<Bill>())).Returns(new Bill());
            
            string exceptionMessage = _faker.Random.Word();
            _ = _orderService.Setup(_ => _.Insert(It.IsAny<Order>())).Throws(new Exception(exceptionMessage));

            OrdersController ordersController = new(_orderService.Object, _waiterService.Object, _billService.Object);

            ActionResult result = ordersController.PostOrder(_orderItems, _faker.Random.Int(), _faker.Random.Int()).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(exceptionMessage, objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndOrder_WhenPostNewOrder()
        {
            _ = _waiterService.Setup(_ => _.Get(It.IsAny<Waiter>())).Returns(new Waiter());
            _ = _billService.Setup(_ => _.GetByTableNumber(It.IsAny<Bill>())).Returns(new Bill());
            _ = _orderService.Setup(_ => _.Insert(It.IsAny<Order>())).Returns(new Order());

            OrdersController ordersController = new(_orderService.Object, _waiterService.Object, _billService.Object);

            ActionResult result = ordersController.PostOrder(_orderItems, _faker.Random.Int(), _faker.Random.Int()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
