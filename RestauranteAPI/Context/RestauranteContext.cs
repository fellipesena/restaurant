using Microsoft.EntityFrameworkCore;
using RestauranteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Context
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions options) : base(options) { }

        public DbSet<Conta> Conta { get; set; } 
        public DbSet<Garcom> Garcom { get; set; } 
        public DbSet<Item> Item { get; set; } 
        public DbSet<ItensPedido> ItensPedido { get; set; } 
        public DbSet<Mesa> Mesa { get; set; } 
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>().HasMany(_conta => _conta.Pedidos).WithOne(_pedido => _pedido.Conta).HasForeignKey(_pedido => _pedido.IdConta);
            modelBuilder.Entity<Pedido>().HasMany(_item => _item.ItensPedido).WithOne(_itemPedido => _itemPedido.Pedido).HasForeignKey(_itemPedido => _itemPedido.IdPedido);
        }
    }
}
