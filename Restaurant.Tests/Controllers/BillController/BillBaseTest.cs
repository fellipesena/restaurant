using Bogus;
using Moq;
using Restaurant.API.Interfaces.Services;

namespace Restaurant.Tests.Controllers.BillController
{
    public class BillBaseTest
    {
        protected readonly Mock<IBillService> _billService;
        protected readonly Mock<ITableService> _tableService;
        protected readonly Faker _faker;

        public BillBaseTest()
        {
            _billService = new Mock<IBillService>();
            _tableService = new Mock<ITableService>();
            _faker = new();
        }
    }
}
