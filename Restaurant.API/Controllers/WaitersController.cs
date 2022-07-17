using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Context.Core;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WaitersController : ControllerBase
    {
        private readonly IWaiterService _waiterService;

        public WaitersController(IWaiterService waiterService) => _waiterService = waiterService;

        /// <summary>
        /// Get all waiters
        /// </summary>
        /// <response code="200">List with all waiters</response>
        [HttpGet]
        public ActionResult<IEnumerable<Waiter>> GetWaiters() =>Ok(_waiterService.GetAll());

        /// <summary>
        /// Create new waiter
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">New waiter created successfully</response>
        [HttpPost]
        public ActionResult<Waiter> PostWaiter(string name)
        {
            Waiter waiter = new() { Name = name };

            waiter = _waiterService.Insert(waiter);

            return Ok(waiter);
        }

        /// <summary>
        /// Delete waiter by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Waiter deleted successfully</response>
        /// <response code="400">Invalid waiter id</response>
        [HttpDelete]
        public ActionResult DeleteWaiter(int id)
        {
            Waiter waiter = new() { Id = id };

            try
            {
                _waiterService.Delete(waiter);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
