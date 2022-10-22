using System.Linq;
using Restaurant.API.Context.Repositories;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;

namespace Restaurant.API.Services
{
    public class BillService : IBillService
    {
        public readonly IUnitOfWork _uow;
        public BillService(IUnitOfWork uow) => _uow = uow;

        public Bill GetByTableNumber(Bill bill) => _uow.Bills.Find(_bill => _bill.Table.Number == bill.Table.Number).FirstOrDefault();

        public Bill StartNew(Bill bill)
        {
            bill.Status = Enums.BillStatus.Openned;
            bill.Value = 0;

            _uow.Bills.Add(bill);
            
            bill.Table.Available = false;

            _uow.Tables.Update(bill.Table);
            _uow.Save();

            return bill;
        }

        public Bill Close(Bill bill)
        {
            bill = GetByTableNumber(bill);

            if (bill == null)
                return bill;

            bill.Status = Enums.BillStatus.Closed;
            bill.Table.Available = true;

            _uow.Bills.Update(bill);
            _uow.Save();

            return bill;
        }
    }
}
