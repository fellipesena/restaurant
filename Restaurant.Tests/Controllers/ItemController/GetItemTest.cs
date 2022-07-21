using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Controllers.ItemController
{
    public class GetItemTest : ItemBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenHaveNoItemWithId()
        {
            _ = _itemService.Setup(_ => _.Get(It.IsAny<Item>())).Returns((Item)null);

            ItemsController itemsController = new(_itemService.Object);

            int itemId = _faker.Random.Int();
            ActionResult result = itemsController.GetItem(itemId).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"Has no item with id {itemId}", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndItem_WhenHaveItemWithId()
        {
            _ = _itemService.Setup(_ => _.Get(It.IsAny<Item>())).Returns(new Item());

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.GetItem(_faker.Random.Int()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
