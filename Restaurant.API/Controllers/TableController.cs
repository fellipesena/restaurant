using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Context.Core;
using Restaurant.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public TableController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Get all tables
        /// </summary>
        /// <response code="200">List with all tables</response>
        [HttpGet]
        public ActionResult<IEnumerable<Table>> GetTables() => _uow.Tables.GetAll().ToList();

        /// <summary>
        /// Get table by number
        /// </summary>
        /// <param name="number"></param>
        /// <response code="200">Table by number</response>
        /// <response code="400">Has no table with number</response>
        [HttpGet("{number}")]
        public ActionResult<Table> GetTable(int number)
        {
            Table table = _uow.Tables.Find(_table => _table.Number == number).FirstOrDefault();

            return table != null ? table : BadRequest($"Table with number {number} was found");
        }

        /// <summary>
        /// Get all availables tables
        /// </summary>
        /// <response code="200">List with all available tables</response>
        [HttpGet("Availables")]
        public ActionResult<IEnumerable<Table>> GetAvailableTables() => _uow.Tables.Find(_table => _table.Available).ToList();

        /// <summary>
        /// Create new table
        /// </summary>
        /// <param name="number"></param>
        /// <response code="200">Table created successfully</response>
        /// <response code="400">Invalid table number</response>
        [HttpPost]
        public ActionResult<Table> PostTableWithNumber(int number)
        {
            Table tableAlreadyExists = _uow.Tables.Find(_mesa => _mesa.Number == number).FirstOrDefault();

            if (tableAlreadyExists != null)
            {
                return BadRequest($"Already exists a table with number {number}");
            }

            Table table = new()
            {
                Number = number,
                Available = true
            };

            _uow.Tables.Add(table);

            _uow.Complete();

            return table;
        }

        /// <summary>
        /// Create new table with next sequence number
        /// </summary>
        /// <response code="200">Table created successfully</response>
        [HttpPost]
        public ActionResult<Table> PostTable()
        {
            int nextTableNumber = _uow.Tables.GetAll().OrderBy(_table => _table.Number).Last().Number;

            Table table = new()
            {
                Number = nextTableNumber,
                Available = true
            };

            _uow.Tables.Add(table);

            _uow.Complete();

            return table;
        }

        /// <summary>
        /// Delete table by number
        /// </summary>
        /// <param name="number"></param>
        /// <response code="200">Table deleted successfully</response>
        /// <response code="400">Invalid table number</response>
        [HttpDelete]
        public ActionResult DeleteMesa(int number)
        {
            Table table = _uow.Tables.Find(_table => _table.Number == number).FirstOrDefault();

            if (table == null)
            {
                return BadRequest($"Has no table with numer {number}");
            }

            _uow.Tables.Remove(table);

            _uow.Complete();

            return Ok();
        }
    }
}
