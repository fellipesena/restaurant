using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Controllers.ItemController
{
    public class PostItemTest : ItemBaseTest
    {
        [Fact]
        public void ShouldReturnOkAndItem_WhenPostNewItem()
        {
            _ = _itemService.Setup(_ => _.Insert(It.IsAny<Item>())).Returns(new Item());

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.PostItem(new Item()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
