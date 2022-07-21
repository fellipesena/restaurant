using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using System;
using Xunit;

namespace Restaurant.Tests.Controllers.TableController
{
    public class DeleteTableByNumberTest : TableBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenTableDoesntExists()
        {
            string exceptionMessage = _faker.Random.Word();
            _ = _tableService.Setup(_ => _.Delete(It.IsAny<Table>())).Throws(new Exception(exceptionMessage));

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.DeleteTable(_faker.Random.Int(), _faker.Random.Int());

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(exceptionMessage, objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOk_WhenDeleteATable()
        {
            _ = _tableService.Setup(_ => _.Delete(It.IsAny<Table>()));

            TablesController tablesController = new(_tableService.Object);

            ActionResult result = tablesController.DeleteTable(_faker.Random.Int(), _faker.Random.Int());

            _ = Assert.IsType<OkResult>(result);
        }
    }
}
