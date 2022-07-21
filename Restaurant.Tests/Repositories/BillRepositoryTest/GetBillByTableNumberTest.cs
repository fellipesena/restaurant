using Xunit;
using System.Linq;
using Restaurant.API.Models;
using Restaurant.API.Enums;
using System.Collections.Generic;

namespace Restaurant.Tests.Repositories.BillRepositoryTest
{
    public class GetBillByTableNumberTest : BaseBillTest
    {
        [Fact]
        public void GetBillById()
        {
            Bill bill =  _billRepository.Get(1);

            Assert.NotNull(bill);
            Assert.Equal(1, bill.Id);
            Assert.Equal(BillStatus.Closed, bill.Status);
        }

        [Fact]
        public void GetAllBills()
        {
            IEnumerable<Bill> bills = _billRepository.GetAll();

            Assert.NotNull(bills);
            Assert.Equal(3, bills.Count());
        }

        [Theory]
        [InlineData(1, BillStatus.Openned)]
        [InlineData(2, BillStatus.Closed)]
        public void FindBill(int count, BillStatus status)
        {
            IEnumerable<Bill> bills = _billRepository.Find(_bill => _bill.Status == status);

            Assert.Equal(count, bills.Count());
        }

        [Fact]
        public void AddBill()
        {
            Bill newBill = new() { Status = BillStatus.Openned, Value = _faker.Random.Decimal() };
            _billRepository.Add(newBill);

            Assert.Equal(4, newBill.Id);
        }

        [Fact]
        public void AddRangeBill()
        {
            List<Bill> bills = new()
            {
                new Bill()
                {
                    Status = API.Enums.BillStatus.Closed,
                    Value = _faker.Random.Decimal(),
                },
                new Bill()
                {
                    Status = API.Enums.BillStatus.Openned,
                    Value = _faker.Random.Decimal(),
                }
            };
            _billRepository.AddRange(bills);

            Assert.Equal(4, bills.First().Id);
            Assert.Equal(5, bills.Last().Id);
        }

        [Fact]
        public void RemoveBill()
        {
            Bill newBill = new() { Status = BillStatus.Openned, Value = _faker.Random.Decimal() };
            _billRepository.Add(newBill);

            _billRepository.Remove(newBill);

            newBill = _billRepository.Get(newBill.Id);

            Assert.Null(newBill);
        }

        // TODO: make this method works
        [Fact]
        public void GetBillByTableNumber()
        {
            int tableNumber = _faker.Random.Int(0, 5);
            Bill newBill = new() { Status = BillStatus.Openned, Value = _faker.Random.Decimal(), Table = new Table() { Number = tableNumber } };
            _billRepository.Add(newBill);

            Bill result = _billRepository.GetBillByTableNumber(tableNumber);

            Assert.Equal(newBill.Id, result.Id);
            Assert.Equal(tableNumber, result.Id);
        }
    }
}
