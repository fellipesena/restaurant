using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Models;

namespace Restaurant.API.Context.Persistence.Repositories
{
    public class OrderItemsRepository : Repository<OrderItems>, IOrderItemsRepository
    {
        public OrderItemsRepository(RestaurantContext context) : base(context) { }
    }
}
