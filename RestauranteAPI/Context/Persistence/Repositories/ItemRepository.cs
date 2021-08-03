using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(RestauranteContext context) : base(context) { }

    }
}
