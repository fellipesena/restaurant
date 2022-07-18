using Bogus;
using Moq;
using Restaurant.API.Interfaces.Services;

namespace Restaurant.Tests.Controllers.WaiterController
{
    public class WaiterBaseTest
    {
        protected readonly Mock<IWaiterService> _waiterService;
        protected readonly Faker _faker;

        public WaiterBaseTest()
        {
            _waiterService = new Mock<IWaiterService>();
            _faker = new();
        }
    }
}
