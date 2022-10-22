using Restaurant.API.Context.Core;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;

namespace Restaurant.API.Services
{
    public class WaiterService : IWaiterService
    {
        public readonly IUnitOfWork _uow;
        public WaiterService(IUnitOfWork uow) => _uow = uow;

        public Waiter Get(Waiter waiter) => _uow.Waiters.Get(waiter.Id);

        public IEnumerable<Waiter> GetAll() => _uow.Waiters.GetAll();

        public Waiter Insert(Waiter waiter)
        {
            _uow.Waiters.Add(waiter);
            _uow.Save();            
            return waiter;
        }

        public void Delete(Waiter waiter)
        {
            waiter = Get(waiter);

            if (waiter == null)
            {
                throw new Exception("Waiter not found");
            }

            _uow.Waiters.Remove(waiter);
            _uow.Save();
        }
    }
}
