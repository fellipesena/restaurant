using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.Tests.Controllers.TableController
{
    public class GetAvailableTablesTest : TableBaseTest
    {
        [Fact]
        public void ShouldReturnOkAndNull_WhenHaveNoAvailableTables()
        {
            _ = _tableService.Setup(_ => _.GetAvailables()).Returns((IEnumerable<Table>)null);

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.GetAvailableTables().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Null(objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndListOfAvailableTables_WhenHaveAvailableTables()
        {
            _ = _tableService.Setup(_ => _.GetAvailables()).Returns(new List<Table>());

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.GetAvailableTables().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
