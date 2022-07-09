using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Models;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(RestaurantContext context) : base(context) { }
    }
}
