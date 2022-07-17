﻿using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService) => _itemService = itemService;

        /// <summary>
        /// Get all items
        /// </summary>
        /// <response code="200">List with all items</response>
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItens() => Ok(_itemService.GetAll());

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <response code="200">Return item by id successfully</response>
        /// <response code="400">Invalid item id</response>
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            Item item = new() { Id = id };

            item = _itemService.Get(item);

            return item == null ? BadRequest($"Has no item with id {id}") : item;
        }

        /// <summary>
        /// Create new item
        /// </summary>
        /// <response code="200">Item created successfully</response>
        [HttpPost]
        public ActionResult<Item> InsertItem(Item item)
        {
            item = _itemService.Insert(item);

            return item;
        }

        /// <summary>
        /// Update a item by id
        /// </summary>
        /// <response code="200">Item updated successfully</response>
        /// <response code="400">Invalid item id</response>
        [HttpPut("{id}")]
        public ActionResult<Item> PatchItem(int id, Item item)
        {
            item.Id = id;

            item = _itemService.Update(item);

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
            Item item = new() { Id = id };

            try
            {
                _itemService.Delete(item);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
