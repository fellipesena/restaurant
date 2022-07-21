using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.Tests.Controllers.TableController
{
    public class GetTablesTest : TableBaseTest
    {
        [Fact]
        public void ShouldReturnOkAndNull_WhenHaveNoTables()
        {
            _ = _tableService.Setup(_ => _.GetAll()).Returns((IEnumerable<Table>)null);

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.GetTables().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Null(objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndListOfTables_WhenHaveTables()
        {
            _ = _tableService.Setup(_ => _.GetAll()).Returns(new List<Table>());

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.GetTables().Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
        }
    }
}
