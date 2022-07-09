using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Models;

namespace Restaurant.API.Context.Persistence.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(RestaurantContext context) : base(context) { }
    }
}
