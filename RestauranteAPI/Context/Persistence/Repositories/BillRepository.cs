using Microsoft.EntityFrameworkCore;
using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Enums;
using RestauranteAPI.Models;
using System.Linq;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(RestaurantContext context) : base(context) { }

        public Bill GetBillByTableNumber(int number) =>
            RestauranteContext.Bill
                .Include(_order => _order.Orders)
                .ThenInclude(_orderItems => _orderItems.OrderItems)
                .ThenInclude(_item => _item.Item)
                .Where(_bill => _bill.Table.Number == number && _bill.Status == BillStatus.Openned)
                .FirstOrDefault();

        public RestaurantContext RestauranteContext => Context as RestaurantContext;
    }
}
