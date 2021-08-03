using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Context.Core;
using RestauranteAPI.Context.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Context.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestauranteContext _context;

        private readonly DbContextOptions _dbContextOptions;

        private readonly string _connectionString;

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbContextOptions = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), _connectionString).Options;
            _context = new RestauranteContext(_dbContextOptions);

            Contas = new ContaRepository(_context);
            Garcons = new GarcomRepository(_context);
            Itens = new ItemRepository(_context);
            ItensPedido = new ItensPedidoRepository(_context);
            Mesas = new MesaRepository(_context);
            Pedidos = new PedidoRepository(_context);
        }

        public IContaRepository Contas { get; private set; }
        public IGarcomRepository Garcons { get; private set; }
        public IItemRepository Itens { get; private set; }
        public IItensPedidoRepository ItensPedido { get; private set; }
        public IMesaRepository Mesas { get; private set; }
        public IPedidoRepository Pedidos { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
