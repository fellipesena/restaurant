using Restaurant.API.Context.Core;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;

namespace Restaurant.API.Services
{
    public class ItemService : IItemService
    {
        public readonly IUnitOfWork _uow;
        public ItemService(IUnitOfWork uow) => _uow = uow;

        public Item Get(Item item) => _uow.Items.Get(item.Id);

        public IEnumerable<Item> GetAll() => _uow.Items.GetAll();

        public Item Insert(Item item)
        {
            _uow.Items.Add(item);

            return item;
        }

        public Item Update(Item item)
        {
            Item newItem = _uow.Items.Get(item.Id);

            if (newItem == null)
            {
                throw new Exception("Item not found");
            }
            if (!string.IsNullOrEmpty(item.Name) && item.Name != newItem.Name)
            {
                newItem.Name = item.Name;
            }
            if (!string.IsNullOrEmpty(item.Description) && item.Description != newItem.Description)
            {
                newItem.Description = item.Description;
            }
            if (!string.IsNullOrEmpty(item.Type) && item.Type != newItem.Type)
            {
                newItem.Type = item.Type;
            }
            if (item.Value != newItem.Value)
            {
                newItem.Value = item.Value;
            }
            if (item.StockQuantity != newItem.StockQuantity)
            {
                newItem.StockQuantity = item.StockQuantity;
            }

            return newItem;
        }

        public void Delete(Item item)
        {
            item = Get(item);

            if (item == null)
            {
                throw new Exception("Item not found");
            }

            _uow.Items.Remove(item);
        }
    }
}
