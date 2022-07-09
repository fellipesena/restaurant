using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Context.Core;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public OrderController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="waiterId"></param>
        /// <param name="tableNumber"></param>
        /// <response code="200">New order created successfully</response>
        /// <response code="400">Failed to proccess the order</response>
        [HttpPost]
        public ActionResult<Order> PostOrder(IEnumerable<OrderItems> orderItems, int waiterId, int tableNumber)
        {
            Waiter waiterIsValid = _uow.Waiters.Get(waiterId);
            if (waiterIsValid == null)
            {
                return BadRequest($"No one waiter with id {waiterId} was found");
            }

            Bill bill = _uow.Bills.GetBillByTableNumber(tableNumber);
            if (bill == null)
            {
                return BadRequest($"No one openned bill to table {tableNumber} was found");
            }

            decimal orderValue = 0;

            foreach (OrderItems itens in orderItems)
            {
                Item item = _uow.Items.Get(itens.ItemId);

                if (item.StockQuantity < itens.Quantity)
                {
                    return BadRequest($"Has no {item.Name} in stock. Available stock quantity is {item.StockQuantity}");
                }

                itens.UnitValue = item.Value;
                itens.TotalValue = item.Value * itens.Quantity;
                item.StockQuantity -= itens.Quantity;

                orderValue += itens.TotalValue;
            }

            Order order = new()
            {
                Value = orderValue,
                WaiterId = waiterId,
                BillId = bill.Id,
                TableId = bill.TableId,
                DateTime = DateTime.Now
            };

            bill.Value += orderValue;

            _uow.Orders.Add(order);

            _uow.Complete();

            foreach (OrderItems itens in orderItems)
            {
                itens.OrderId = order.Id;
            }

            _uow.OrderItems.AddRange(orderItems);

            _uow.Complete();

            return order;
        }
    }
}
