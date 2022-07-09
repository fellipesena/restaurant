using Microsoft.EntityFrameworkCore;
using RestauranteAPI.Models;

namespace RestauranteAPI.Context
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions options) : base(options) { }

        public DbSet<Bill> Bill { get; set; }
        public DbSet<Waiter> Waiter { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Bill>().HasMany(_bill => _bill.Orders).WithOne(_order => _order.Bill).HasForeignKey(_order => _order.BillId);
            _ = modelBuilder.Entity<Order>().HasMany(_item => _item.OrderItems).WithOne(_orderItem => _orderItem.Order).HasForeignKey(_orderItem => _orderItem.OrderId);
        }
    }
}
