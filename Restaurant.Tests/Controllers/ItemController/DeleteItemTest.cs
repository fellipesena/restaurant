using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System;
using Xunit;

namespace Restaurant.Tests.Controllers.ItemController
{
    public class DeleteItemTest : ItemBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenItemDoesntExists()
        {
            string exceptionMessage = _faker.Random.Word();
            _ = _itemService.Setup(_ => _.Delete(It.IsAny<Item>())).Throws(new Exception(exceptionMessage));

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.DeleteItem(_faker.Random.Int());

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(exceptionMessage, objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOk_WhenDeleteAItem()
        {
            _ = _itemService.Setup(_ => _.Delete(It.IsAny<Item>()));

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.DeleteItem(_faker.Random.Int());

            _ = Assert.IsType<OkResult>(result);
        }
    }
}
