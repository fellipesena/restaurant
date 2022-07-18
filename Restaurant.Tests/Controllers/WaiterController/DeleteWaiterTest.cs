using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System;
using Xunit;

namespace Restaurant.Tests.Controllers.WaiterController
{
    public class DeleteWaiterTest : WaiterBaseTest
    {
        [Fact]
        public void ShouldReturnOk_WhenDeleteWaiter()
        {
            _ = _waiterService.Setup(_ => _.Delete(It.IsAny<Waiter>()));

            WaitersController waitersController = new(_waiterService.Object);
            
            int id = _faker.Random.Int();
            ActionResult result = waitersController.DeleteWaiter(id);

            _ = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenDeleteWaiterWithInvalidId()
        {
            string exceptionMessage = _faker.Random.Word();
            _ = _waiterService.Setup(_ => _.Delete(It.IsAny<Waiter>())).Throws(new Exception(exceptionMessage));

            WaitersController waitersController = new(_waiterService.Object);

            int id = _faker.Random.Int();
            ActionResult result = waitersController.DeleteWaiter(id);

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(exceptionMessage, objectResult.Value);
        }
    }
}
