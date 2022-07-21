using Bogus;
using Moq;
using Restaurant.API.Interfaces.Services;

namespace Restaurant.Tests.Controllers.ItemController
{
    public class ItemBaseTest
    {
        protected readonly Mock<IItemService> _itemService;
        protected readonly Faker _faker;

        public ItemBaseTest()
        {
            _itemService = new Mock<IItemService>();
            _faker = new();
        }
    }
}
