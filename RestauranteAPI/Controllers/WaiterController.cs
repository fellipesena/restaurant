using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Context.Core;
using RestauranteAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestauranteAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WaiterController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public WaiterController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Get all waiters
        /// </summary>
        /// <response code="200">List with all waiters</response>
        [HttpGet]
        public ActionResult<IEnumerable<Waiter>> GetWaiters() => _uow.Waiters.GetAll().ToList();

        /// <summary>
        /// Create new waiter
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">New waiter created successfully</response>
        [HttpPost]
        public ActionResult<Waiter> PostWaiter(string name)
        {
            Waiter waiter = new()
            {
                Name = name
            };

            _uow.Waiters.Add(waiter);

            _uow.Complete();

            return waiter;
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
            Waiter waiter = _uow.Waiters.Get(id);

            if (waiter == null)
            {
                return BadRequest($"Has no waiter with id {id}");
            }

            _uow.Waiters.Remove(waiter);

            _uow.Complete();

            return Ok();
        }
    }
}
