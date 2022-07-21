using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System;
using Xunit;

namespace Restaurant.Tests.Controllers.ItemController
{
    public class PatchItemTest : ItemBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenItemDoesntExists()
        {
            string exceptionMessage = _faker.Random.Word();
            _ = _itemService.Setup(_ => _.Update(It.IsAny<Item>())).Throws(new Exception(exceptionMessage));

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.PatchItem(_faker.Random.Int(), new Item()).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(exceptionMessage, objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndItem_WhenPatchAItem()
        {
            _ = _itemService.Setup(_ => _.Update(It.IsAny<Item>())).Returns(new Item());

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.PatchItem(_faker.Random.Int(), new Item()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
