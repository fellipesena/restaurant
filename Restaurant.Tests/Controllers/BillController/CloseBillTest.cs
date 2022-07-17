using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Controllers.BillController
{
    public class CloseBillTest : BillBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenHaveNoBillToTable()
        {
            _ = _billService.Setup(_ => _.Close(It.IsAny<Bill>())).Returns((Bill)null);

            BillsController billsController = new(_billService.Object, _tableService.Object);

            int tableNumber = _faker.Random.Int();
            ActionResult result = billsController.CloseBill(tableNumber).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"No bill to table {tableNumber} was found", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndBill_WhenTableIsAvailable()
        {
            _ = _billService.Setup(_ => _.Close(It.IsAny<Bill>())).Returns(new Bill());

            BillsController billsController = new(_billService.Object, _tableService.Object);

            int tableNumber = _faker.Random.Int();
            ActionResult result = billsController.CloseBill(tableNumber).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
            Bill bill = Assert.IsType<Bill>(objectResult.Value);
        }
    }
}
