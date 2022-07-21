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
        public void ShouldReturnBadRequest_WhenHaveNoBillOpenned()
        {
            _ = _billService.Setup(_ => _.GetByTableNumber(It.IsAny<Bill>())).Returns((Bill)null);

            BillsController billsController = new(_billService.Object, _tableService.Object);

            ActionResult result = billsController.GetBill(_faker.Random.Int()).Result;

            BadRequestObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("Table has no one bill openned", objectResult.Value);
        }

        [Fact]
        public void ShouldReturnOkAndBill_WhenHaveBillOpenned()
        {
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
