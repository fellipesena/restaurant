using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Context.Core;
using Restaurant.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ItemController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Get all items
        /// </summary>
        /// <response code="200">List with all items</response>
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItens() => _uow.Items.GetAll().ToList();

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <response code="200">Return item by id successfully</response>
        /// <response code="400">Invalid item id</response>
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            Item item = _uow.Items.Get(id);

            return item == null ? BadRequest($"Has no item with id {id}") : item;
        }

        /// <summary>
        /// Create new item
        /// </summary>
        /// <response code="200">Item created successfully</response>
        [HttpPost]
        public ActionResult<Item> PostItem(Item item)
        {
            _uow.Items.Add(item);

            _uow.Complete();

            return item;
        }

        /// <summary>
        /// Update a item by id
        /// </summary>
        /// <response code="200">Item updated successfully</response>
        /// <response code="400">Invalid item id</response>
        [HttpPut("{id}")]
        public ActionResult<Item> PutItem(int id, Item item)
        {
            Item actualItem = _uow.Items.Get(id);
            if (actualItem == null)
            {
                return BadRequest($"Has no item with id {id}");
            }
            actualItem.Name = item.Name;
            actualItem.Description = item.Description;
            actualItem.Type = item.Type;
            actualItem.Value = item.Value;
            actualItem.StockQuantity = item.StockQuantity;

            _uow.Complete();

            return item;
        }

        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <response code="200">Item deleted successfully</response>
        /// <response code="400">Invalid item id</response>
        [HttpDelete]
        public ActionResult DeleteItem(int id)
        {
            Item item = _uow.Items.Get(id);

            if (item == null)
            {
                return BadRequest($"Has no item with id {id}");
            }

            _uow.Items.Remove(item);

            _uow.Complete();

            return Ok();
        }
    }
}
