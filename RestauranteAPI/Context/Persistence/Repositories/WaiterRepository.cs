using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Models;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class WaiterRepository : Repository<Waiter>, IWaiterRepository
    {
        public WaiterRepository(RestaurantContext context) : base(context) { }
    }
}