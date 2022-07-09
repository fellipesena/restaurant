using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Models;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(RestaurantContext context) : base(context) { }
    }
}
