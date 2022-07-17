using Restaurant.API.Models;
using System.Collections.Generic;

namespace Restaurant.API.Interfaces.Services
{
    public interface IItemService
    {
        public IEnumerable<Item> GetAll();
        public Item Get(Item item);
        public Item Insert(Item item);
        public Item Update(Item item);
        public void Delete(Item item);
    }
}
