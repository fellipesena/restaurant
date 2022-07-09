﻿using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Models;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(RestaurantContext context) : base(context) { }
    }
}
