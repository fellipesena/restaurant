using RestauranteAPI.Models;

namespace RestauranteAPI.Context.Core.Repositories
{
    public interface IBillRepository : IRepository<Bill>
    {
        Bill GetBillByTableNumber(int number);
    }
}
