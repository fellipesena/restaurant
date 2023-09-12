using Restaurant.API.Context.Repositories;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;

namespace Restaurant.API.Services
{
    public class OrderService : IOrderService
    {
        public readonly IUnitOfWork _uow;
        public OrderService(IUnitOfWork uow) => _uow = uow;

        public Order Insert(Order order)
        {
            Order newOrder = new(order.Waiter.Id, order.Bill.Id, order.Bill.TableId);

            _uow.Orders.Add(newOrder);

            foreach (OrderItems items in order.OrderItems)
            {
                Item item = _uow.Items.Get(items.ItemId);

                item.RemoveFromStock(items.Quantity);
                _uow.Items.Update(item);

                items.StartNewOrderItem(item.Value, newOrder.Id);

                newOrder.IncreaseTotalValue(items.TotalValue);
            }

            order.Bill.IncreaseTotalValue(newOrder.Value);

            _uow.Bills.Update(order.Bill);

            _uow.OrderItems.AddRange(order.OrderItems);

            _uow.Save();

            return order;
        }
    }
}
