using Microsoft.EntityFrameworkCore;
using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Enums;
using Restaurant.API.Models;
using System.Linq;

namespace Restaurant.API.Context.Persistence.Repositories
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(RestaurantContext context) : base(context) { }

        public Bill GetBillByTableNumber(int number) =>
            RestaurantContext.Bill
            .Include(_bill => _bill.Table)
            .Where(_bill => _bill.Table.Number == number)
            .Include(_bill => _bill.Orders)
                .ThenInclude(_orders => _orders.OrderItems)
                .ThenInclude(_orderItems => _orderItems.Item)
            .FirstOrDefault();

        public RestaurantContext RestaurantContext => Context as RestaurantContext;
    }
}
