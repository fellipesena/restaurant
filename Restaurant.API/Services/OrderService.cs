using Restaurant.API.Context.Repositories;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;

namespace Restaurant.API.Services
{
    public class OrderService : IOrderService
    {
        public readonly IUnitOfWork _uow;
        public OrderService(IUnitOfWork uow) => _uow = uow;

        public Order Insert(Order order)
        {
            decimal orderValue = 0;

            foreach (OrderItems itens in order.OrderItems)
            {
                Item item = _uow.Items.Get(itens.ItemId);

                if (item.StockQuantity < itens.Quantity)
                {
                    throw new Exception($"Has no {item.Name} in stock. Available stock quantity is {item.StockQuantity}");
                }

                itens.UnitValue = item.Value;
                itens.TotalValue = item.Value * itens.Quantity;
                item.StockQuantity -= itens.Quantity;

                _uow.Items.Update(item);

                orderValue += itens.TotalValue;
            }

            order.Value = orderValue;
            order.WaiterId = order.Waiter.Id;
            order.BillId = order.Bill.Id;
            order.TableId = order.Bill.TableId;
            order.DateTime = System.DateTime.Now;

            _uow.Orders.Add(order);

            order.Bill.Value += orderValue;

            _uow.Bills.Update(order.Bill);

            foreach (OrderItems itens in order.OrderItems)
            {
                itens.OrderId = order.Id;
            }

            _uow.OrderItems.AddRange(order.OrderItems);

            _uow.Save();

            return order;
        }
    }
}
