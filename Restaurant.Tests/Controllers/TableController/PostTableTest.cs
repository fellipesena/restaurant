using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System;
using Xunit;

namespace Restaurant.Tests.Controllers.TableController
{
    public class PostTableTest : TableBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenTableAlreadyExists()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns(new Table());

            TablesController tablesController = new(_tableService.Object);

            int tableNumber = _faker.Random.Int();
            ActionResult result = tablesController.PostTable(tableNumber).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"Already exists a table with number {tableNumber}", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndTable_WhenPostNewTable()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns((Table)null);
            _ = _tableService.Setup(_ => _.Insert(It.IsAny<Table>())).Returns(new Table());

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.PostTable(_faker.Random.Int()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
