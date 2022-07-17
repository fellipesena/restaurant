using Restaurant.API.Models;

namespace Restaurant.API.Context.Core.Repositories
{
    public interface IBillRepository : IRepository<Bill>
    {
        Bill GetBillByTableNumber(int number);
    }
}
