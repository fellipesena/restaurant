using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Controllers.WaiterController
{
    public class PostWaiterTest : WaiterBaseTest
    {
        [Fact]
        public void ShouldReturnOkAndWaiter_WhenPostNewWaiter()
        {
            _ = _waiterService.Setup(_ => _.Insert(It.IsAny<Waiter>())).Returns(new Waiter());

            WaitersController waiterController = new(_waiterService.Object);

            ActionResult result = waiterController.PostWaiter(_faker.Random.Word()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
