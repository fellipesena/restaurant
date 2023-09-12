using Restaurant.API.Context.Repositories;
using Restaurant.API.Enums;
using Restaurant.API.Exceptions;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System.Collections.Generic;

namespace Restaurant.API.Services
{
    public class ItemService : IItemService
    {
        private readonly EntityType entityType = EntityType.Item;
        private readonly IUnitOfWork _uow;

        public ItemService(IUnitOfWork uow) => _uow = uow;

        public Item Get(Item item) => _uow.Items.Get(item.Id);

        public IEnumerable<Item> GetAll() => _uow.Items.GetAll();

        public Item Insert(Item item)
        {
            _uow.Items.Add(item);
            _uow.Save();

            return item;
        }

        public Item Update(Item item)
        {
            Item newItem = _uow.Items.Get(item.Id);

            if (newItem == null)
                throw new NotFoundException(entityType, item.Id.ToString());

            newItem.SetPropertiesToUpdate(item);

            _uow.Items.Update(newItem);
            _uow.Save();

            return newItem;
        }

        public void Delete(Item item)
        {
            item = Get(item);

            if (item == null)
                throw new NotFoundException(entityType, item.Id.ToString());

            _uow.Items.Remove(item);
            _uow.Save();
        }
    }
}
