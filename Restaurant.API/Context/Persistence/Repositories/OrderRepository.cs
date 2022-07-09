using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Models;

namespace Restaurant.API.Context.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(RestaurantContext context) : base(context) { }
    }
}
