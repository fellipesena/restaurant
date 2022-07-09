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
                .Include(_order => _order.Orders)
                .ThenInclude(_orderItems => _orderItems.OrderItems)
                .ThenInclude(_item => _item.Item)
                .Where(_bill => _bill.Table.Number == number && _bill.Status == BillStatus.Openned)
                .FirstOrDefault();

        public RestaurantContext RestaurantContext => Context as RestaurantContext;
    }
}
