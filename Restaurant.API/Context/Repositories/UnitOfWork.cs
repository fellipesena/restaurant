using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant.API.Models;
using System;

namespace Restaurant.API.Context.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantContext _context;
        private readonly DbContextOptions _dbContextOptions;
        private readonly string _connectionString;

        public IRepository<Bill> Bills { get; private set; }
        public IRepository<Waiter> Waiters { get; private set; }
        public IRepository<Item> Items { get; private set; }
        public IRepository<OrderItems> OrderItems { get; private set; }
        public IRepository<Table> Tables { get; private set; }
        public IRepository<Order> Orders { get; private set; }

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbContextOptions = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), _connectionString).Options;
            _context = new RestaurantContext(_dbContextOptions);

            Bills = new Repository<Bill>(_context);
            Waiters = new Repository<Waiter>(_context);
            Items = new Repository<Item>(_context);
            OrderItems = new Repository<OrderItems>(_context);
            Tables = new Repository<Table>(_context);
            Orders = new Repository<Order>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
