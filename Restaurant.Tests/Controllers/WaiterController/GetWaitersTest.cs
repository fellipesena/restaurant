using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.Tests.Controllers.WaiterController
{
    public class GetWaitersTest : WaiterBaseTest
    {
        [Fact]
        public void ShouldReturnOkAndNull_WhenHaveNoWaiters()
        {
            _ = _waiterService.Setup(_ => _.GetAll()).Returns((IEnumerable<Waiter>)null);

            WaitersController waitersController = new(_waiterService.Object);

            ActionResult result = waitersController.GetWaiters().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Null(objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndListOfWaiters_WhenHaveWaiters()
        {
            _ = _waiterService.Setup(_ => _.GetAll()).Returns(new List<Waiter>());

            WaitersController waitersController = new(_waiterService.Object);

            ActionResult result = waitersController.GetWaiters().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
