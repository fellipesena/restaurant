using Restaurant.API.Models;
using System.Collections.Generic;

namespace Restaurant.API.Interfaces.Services
{
    public interface IWaiterService
    {
        public Waiter Get(Waiter waiter);
        public IEnumerable<Waiter> GetAll();
        public Waiter Insert(Waiter waiter);
        public void Delete(Waiter waiter);
    }
}
