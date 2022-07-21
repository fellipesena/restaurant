using Bogus;
using Restaurant.API.Context.Persistence.Repositories;
using Restaurant.API.Models;
using System.Collections.Generic;

namespace Restaurant.Tests.Repositories.BillRepositoryTest
{
    public class BaseBillTest : BaseRestaurantContext
    {
        protected readonly BillRepository _billRepository;
        protected readonly Faker _faker;
        
        public BaseBillTest()
        {
            _faker = new Faker();
            _billRepository = new BillRepository(_context);

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
                },
                new Bill()
                {
                    Status = API.Enums.BillStatus.Closed,
                    Value = _faker.Random.Decimal(),
                },
            };

            _context.AddRange(bills);
            _ = _context.SaveChanges();
        }
    }
}
