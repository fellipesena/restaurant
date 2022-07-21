using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService) => _tableService = tableService;

        /// <summary>
        /// Get all tables
        /// </summary>
        /// <response code="200">List with all tables</response>
        [HttpGet]
        public ActionResult<IEnumerable<Table>> GetTables() => Ok(_tableService.GetAll());

        /// <summary>
        /// Get table by number
        /// </summary>
        /// <param name="number"></param>
        /// <response code="200">Table by number</response>
        /// <response code="400">Has no table with number</response>
        [HttpGet("{number}")]
        public ActionResult<Table> GetTable(int number)
        {
            Table table = new() { Number = number };
            table = _tableService.GetByNumber(table);

            return table != null ? Ok(table) : BadRequest($"Has no table with number {number}");
        }

        /// <summary>
        /// Get all availables tables
        /// </summary>
        /// <response code="200">List with all available tables</response>
        [HttpGet("Availables")]
        public ActionResult<IEnumerable<Table>> GetAvailableTables() => Ok(_tableService.GetAvailables());

        /// <summary>
        /// Create new table, if dont send number so will be next number
        /// </summary>
        /// <param name="number"></param>
        /// <response code="200">Table created successfully</response>
        /// <response code="400">Invalid table number</response>
        [HttpPost]
        public ActionResult<Table> PostTable(int? number)
        {
            Table table = new() { Number = number ?? 0 };
            Table tableExists = _tableService.GetByNumber(table);

            if (tableExists != null)
            {
                return BadRequest($"Already exists a table with number {number}");
            }

            table = _tableService.Insert(table);

            return Ok(table);
        }

        /// <summary>
        /// Delete table by id or number
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <response code="200">Table deleted successfully</response>
        /// <response code="400">Invalid table number</response>
        [HttpDelete]
        public ActionResult DeleteTable(int? id, int? number)
        {
            Table table = new() { Id = id ?? 0, Number = number ?? 0 };

            try
            {
                _tableService.Delete(table);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
