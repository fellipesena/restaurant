using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Controllers.BillController
{
    public class OpenBillTest : BillBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenHaveNoTableWithNumber()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns((Table)null);

            BillsController billsController = new(_billService.Object, _tableService.Object);
            
            int tableNumber = _faker.Random.Int();
            ActionResult result = billsController.OpenBill(tableNumber).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"No table with number {tableNumber} was found", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenTableIsNotAvailable()
        {
            Table table = new() { Available = false };
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns(table);

            BillsController billsController = new(_billService.Object, _tableService.Object);

            int tableNumber = _faker.Random.Int();
            ActionResult result = billsController.OpenBill(tableNumber).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"Table {tableNumber} is not available", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndBill_WhenTableIsAvailable()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns(new Table());
            _ = _billService.Setup(_ => _.StartNew(It.IsAny<Bill>())).Returns(new Bill());

            BillsController billsController = new(_billService.Object, _tableService.Object);

            int tableNumber = _faker.Random.Int();
            ActionResult result = billsController.OpenBill(tableNumber).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(objectResult.Value);
            Bill bill = Assert.IsType<Bill>(objectResult.Value);
        }
    }
}
