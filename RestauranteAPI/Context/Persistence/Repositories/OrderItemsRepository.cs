using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Models;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class OrderItemsRepository : Repository<OrderItems>, IOrderItemsRepository
    {
        public OrderItemsRepository(RestaurantContext context) : base(context) { }
    }
}
