using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.Tests.Controllers.ItemController
{
    public class GetItemsTest : ItemBaseTest
    {
        [Fact]
        public void ShouldReturnOkAndNull_WhenHaveNoItems()
        {
            _ = _itemService.Setup(_ => _.GetAll()).Returns((IEnumerable<Item>)null);

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.GetItems().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Null(objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndListOfItems_WhenHaveItems()
        {
            _ = _itemService.Setup(_ => _.GetAll()).Returns(new List<Item>());

            ItemsController itemsController = new(_itemService.Object);

            ActionResult result = itemsController.GetItems().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
