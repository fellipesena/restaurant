using Restaurant.API.Context.Core;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.API.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _uow;
        public TableService(IUnitOfWork uow) => _uow = uow;

        public Table Insert(Table table)
        {
            if (table.Number == 0)
            {
                int lastTableNumber = _uow.Tables.GetAll().OrderBy(_table => _table.Number).LastOrDefault()?.Number ?? 0;
                table.Number = lastTableNumber + 1;
            }

            _uow.Tables.Add(table);

            return table;
        }

        public IEnumerable<Table> GetAll() => _uow.Tables.GetAll();

        public Table GetByNumber(Table table) => _uow.Tables.Find(_table => _table.Number == table.Number).FirstOrDefault();

        public IEnumerable<Table> GetAvailables() => _uow.Tables.Find(_table => _table.Available);

        public void Delete(Table table)
        {
            table = _uow.Tables.Find(_table => _table.Id == table.Id || _table.Number == table.Number).FirstOrDefault();

            if (table == null)
            {
                throw new Exception("Table not found");
            }

            _uow.Tables.Remove(table);
        }
    }
}
