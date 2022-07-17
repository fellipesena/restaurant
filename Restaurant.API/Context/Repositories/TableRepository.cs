using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Models;

namespace Restaurant.API.Context.Persistence.Repositories
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(RestaurantContext context) : base(context) { }
    }
}
