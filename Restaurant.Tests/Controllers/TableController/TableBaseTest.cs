using Bogus;
using Moq;
using Restaurant.API.Interfaces.Services;

namespace Restaurant.Tests.Controllers.TableController
{
    public class TableBaseTest
    {
        protected readonly Mock<ITableService> _tableService;
        protected readonly Faker _faker;

        public TableBaseTest()
        {
            _tableService = new Mock<ITableService>();
            _faker = new();
        }
    }
}
