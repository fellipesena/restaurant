﻿using Restaurant.API.Models;
using System;

namespace Restaurant.API.Context.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Bill> Bills { get; }
        IRepository<Waiter> Waiters { get; }
        IRepository<Item> Items { get; }
        IRepository<OrderItems> OrderItems { get; }
        IRepository<Table> Tables { get; }
        IRepository<Order> Orders { get; }

        void Save();
    }
}
