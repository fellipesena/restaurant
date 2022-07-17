using Restaurant.API.Models;

namespace Restaurant.API.Interfaces.Services
{
    public interface IBillService
    {
        public Bill GetByTableNumber(Bill bill);
        public Bill StartNew(Bill bill);
        public Bill Close(Bill bill);
    }
}
