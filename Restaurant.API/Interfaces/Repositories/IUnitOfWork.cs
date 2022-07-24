using Restaurant.API.Context.Core.Repositories;
using System;

namespace Restaurant.API.Context.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBillRepository Bills { get; }
        IWaiterRepository Waiters { get; }
        IItemRepository Items { get; }
        IOrderItemsRepository OrderItems { get; }
        ITableRepository Tables { get; }
        IOrderRepository Orders { get; }
        void Attach<TEntity>(TEntity entity) where TEntity : class;
    }
}
