using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Models;

namespace Restaurant.API.Context.Persistence.Repositories
{
    public class WaiterRepository : Repository<Waiter>, IWaiterRepository
    {
        public WaiterRepository(RestaurantContext context) : base(context) { }
    }
}