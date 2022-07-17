using Restaurant.API.Context.Core;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;

namespace Restaurant.API.Services
{
    public class BillService : IBillService
    {
        public readonly IUnitOfWork _uow;
        public BillService(IUnitOfWork uow) => _uow = uow;

        public Bill GetByTableNumber(Bill bill) => _uow.Bills.GetBillByTableNumber(bill.Table.Number);

        public Bill StartNew(Bill bill)
        {
            bill.Status = Enums.BillStatus.Openned;
            bill.Value = 0;

            _uow.Bills.Add(bill);

            bill.Table.Available = false;

            _uow.Complete();

            return bill;
        }

        public Bill Close(Bill bill)
        {
            bill = GetByTableNumber(bill);

            if (bill == null)
                return bill;

            bill.Status = Enums.BillStatus.Closed;
            bill.Table.Available = true;

            _uow.Complete();

            return bill;
        }
    }
}
