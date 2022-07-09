using RestauranteAPI.Context.Core.Repositories;
using System;

namespace RestauranteAPI.Context.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBillRepository Bills { get; }
        IWaiterRepository Waiters { get; }
        IItemRepository Items { get; }
        IOrderItemsRepository OrderItems { get; }
        ITableRepository Tables { get; }
        IOrderRepository Orders { get; }
        void Complete();
    }
}
