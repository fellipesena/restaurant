using Restaurant.API.Models;

namespace Restaurant.API.Interfaces.Services
{
    public interface IOrderService
    {
        public Order Insert(Order order);
    }
}
