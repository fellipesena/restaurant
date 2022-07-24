using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant.API.Context.Core;
using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Context.Persistence.Repositories;
using System;

namespace Restaurant.API.Context.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantContext _context;

        private readonly DbContextOptions _dbContextOptions;

        private readonly string _connectionString;

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbContextOptions = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), _connectionString).Options;
            _context = new RestaurantContext(_dbContextOptions);

            Bills = new BillRepository(_context);
            Waiters = new WaiterRepository(_context);
            Items = new ItemRepository(_context);
            OrderItems = new OrderItemsRepository(_context);
            Tables = new TableRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public IBillRepository Bills { get; private set; }
        public IWaiterRepository Waiters { get; private set; }
        public IItemRepository Items { get; private set; }
        public IOrderItemsRepository OrderItems { get; private set; }
        public ITableRepository Tables { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public void Attach<TEntity>(TEntity entity) where TEntity : class => _context.Set<TEntity>().Attach(entity);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
