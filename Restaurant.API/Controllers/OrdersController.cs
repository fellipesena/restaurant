using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IWaiterService _waiterService;
        private readonly IBillService _billService;

        public OrdersController(IOrderService orderService, IWaiterService waiterService, IBillService billService)
        {
            _orderService = orderService;
            _waiterService = waiterService;
            _billService = billService;
        }

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
            Waiter waiter = new() { Id = waiterId };
            waiter = _waiterService.Get(waiter);
            if (waiter == null)
            {
                return BadRequest($"No one waiter with id {waiterId} was found");
            }

            Bill bill = new() { TableId = tableNumber };
            bill = _billService.GetByTableNumber(bill);
            if (bill == null)
            {
                return BadRequest($"No one openned bill to table {tableNumber} was found");
            }

            Order order = new() { Waiter = waiter, Bill = bill, OrderItems = orderItems };

            try
            {
                order = _orderService.Insert(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(order);
        }
    }
}
