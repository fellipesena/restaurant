using Restaurant.API.Models;
using System.Collections.Generic;

namespace Restaurant.API.Interfaces.Services
{
    public interface ITableService
    {
        public Table Insert(Table table);
        public IEnumerable<Table> GetAll();
        public Table GetByNumber(Table table);
        public IEnumerable<Table> GetAvailables();
        public void Delete(Table table);
    }
}
