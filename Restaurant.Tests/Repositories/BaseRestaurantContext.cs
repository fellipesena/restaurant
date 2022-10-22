using Microsoft.EntityFrameworkCore;
using Restaurant.API.Context;
using System;

namespace Restaurant.Tests.Repositories
{
    public class BaseRestaurantContext
    {
        protected readonly RestaurantContext _context;
        public BaseRestaurantContext()
        {
            DbContextOptions<RestaurantContext> options = new DbContextOptionsBuilder<RestaurantContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new(options);
            _ = _context.Database.EnsureCreated();
        }
    }
}
