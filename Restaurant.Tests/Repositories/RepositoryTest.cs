using System.Linq;
using System.Collections.Generic;
using Bogus;
using Restaurant.API.Context.Core.Repositories;
using Restaurant.API.Context.Persistence.Repositories;
using Restaurant.API.Models;
using Xunit;

namespace Restaurant.Tests.Repositories
{
    public class RepositoryTest : BaseRestaurantContext
    {
        protected readonly IRepository<Bill> _repository;
        protected readonly Faker _faker;

        public RepositoryTest()
        {
            _repository = new Repository<Bill>(_context);
            _faker = new();
        }

        [Fact]
        public void ShouldReturnEntityWithId_WhenAdd()
        {
            Bill bill = new() { Value = _faker.Random.Decimal() };

            _repository.Add(bill);
            _context.SaveChanges();

            Assert.Equal(1, bill.Id);
        }

        [Fact]
        public void ShouldReturnEntityWithIds_WhenAddRange()
        {
            List<Bill> bills = new List<Bill>();
            
            bills.Add(new() { Value = _faker.Random.Decimal() });
            bills.Add(new() { Value = _faker.Random.Decimal() });

            _repository.AddRange(bills);
            _context.SaveChanges();

            Assert.Equal(1, bills[0].Id);
            Assert.Equal(2, bills[1].Id);
        }

        [Fact]
        public void ShouldReturnEntity_WhenGet()
        {
            Bill bill = new() { Value = _faker.Random.Decimal() };

            _repository.Add(bill);
            _context.SaveChanges();

            Bill actualBill = _repository.Get(bill.Id);

            Assert.Equal(actualBill, bill);
        }

        [Fact]
        public void ShouldReturnEntities_WhenGetAll()
        {
            List<Bill> bills = new List<Bill>();
            
            bills.Add(new() { Value = _faker.Random.Decimal() });
            bills.Add(new() { Value = _faker.Random.Decimal() });

            _repository.AddRange(bills);
            _context.SaveChanges();

            IEnumerable<Bill> actualBills = _repository.GetAll();

            Assert.Equal(actualBills, bills);
        }

        [Fact]
        public void ShouldReturnEntity_WhenFind()
        {
            decimal value = _faker.Random.Decimal();

            Bill bill = new() { Value = value };

            _repository.Add(bill);
            _context.SaveChanges();

            Bill actualBill = _repository.Find(bill => bill.Value == value).FirstOrDefault();

            Assert.Equal(actualBill, bill);
        }

        [Fact]
        public void ShouldChangeEntity_WhenUpdate()
        {
            Bill bill = new() { Value = _faker.Random.Decimal() };

            _repository.Add(bill);
            _context.SaveChanges();

            decimal newValue = _faker.Random.Decimal();
            bill.Value = newValue;
            
            _repository.Update(bill);
            _context.SaveChanges();

            Bill actualBill = _repository.Get(bill.Id);

            Assert.Equal(actualBill.Value, newValue);
        }

        [Fact]
        public void ShouldDeleteEntity_WhenRemove()
        {
            decimal value = _faker.Random.Decimal();

            Bill bill = new() { Value = value };

            _repository.Add(bill);
            _context.SaveChanges();

            _repository.Remove(bill);
            _context.SaveChanges();

            Bill actualBill = _repository.Get(bill.Id);

            Assert.Null(actualBill);
        }
    }
}
