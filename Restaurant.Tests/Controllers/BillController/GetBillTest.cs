using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Restaurant.API.Controllers;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Controllers.BillController
{
    public class GetBillTest : BillBaseTest
    {
        [Fact]
        public void ShouldReturnBadRequest_WhenHaveNoTable()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns((Table)null);

            BillsController billsController = new(_billService.Object, _tableService.Object);

            int tableNumber = _faker.Random.Int();
            ActionResult result = billsController.GetBill(tableNumber).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"Has no table with number {tableNumber}", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenHaveNoBillToTable()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns(new Table());
            _ = _billService.Setup(_ => _.GetByTableNumber(It.IsAny<Bill>())).Returns((Bill)null);

            BillsController billsController = new(_billService.Object, _tableService.Object);

            ActionResult result = billsController.GetBill(_faker.Random.Int()).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("Table has no one bill", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndBill_WhenHaveBillOpenned()
        {
            _ = _tableService.Setup(_ => _.GetByNumber(It.IsAny<Table>())).Returns(new Table());
            _ = _billService.Setup(_ => _.GetByTableNumber(It.IsAny<Bill>())).Returns(new Bill());

            BillsController billsController = new(_billService.Object, _tableService.Object);

            ActionResult result = billsController.GetBill(_faker.Random.Int()).Result;

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            string objectResultString = JsonConvert.SerializeObject(objectResult.Value);
            string expectedString = JsonConvert.SerializeObject(new Bill());
            Assert.Equal(expectedString, objectResultString);
        }
    }
}
