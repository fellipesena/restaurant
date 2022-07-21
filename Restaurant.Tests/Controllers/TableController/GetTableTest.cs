using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Controllers.TableController
{
    public class GetTableTest : TableBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenHaveNoTableWithNumber()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns((Table)null);

            TablesController tablesController = new(_tableService.Object);

            int tableNumber = _faker.Random.Int();
            ActionResult result = tablesController.GetTable(tableNumber).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"Has no table with number {tableNumber}", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndTable_WhenHaveTableWithNumber()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns(new Table());

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.GetTable(_faker.Random.Int()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
