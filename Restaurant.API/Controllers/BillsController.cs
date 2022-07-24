using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Interfaces.Services;
using Restaurant.API.Models;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BillsController : Controller
    {
        private readonly IBillService _billService;
        private readonly ITableService _tableService;

        public BillsController(IBillService billService, ITableService tableService)
        {
            _billService = billService;
            _tableService = tableService;
        }

        /// <summary>
        /// Get bill by table number
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <response code="200">Bill by number of table</response>
        /// <response code="400">Table has no bill openned</response>
        [HttpGet("{tableNumber}")]
        public ActionResult<Bill> GetBill(int tableNumber)
        {
            Table table = new() { Number = tableNumber };
            table = _tableService.GetByNumber(table);

            if (table == null)
                return BadRequest($"Has no table with number {tableNumber}");

            Bill bill = new() { Table = table };
            bill = _billService.GetByTableNumber(bill);

            return bill != null ? Ok(bill) : BadRequest("Table has no one bill openned");
        }

        /// <summary>
        /// Create new bill
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <response code="200">Bill created successfully</response>
        /// <response code="400">Failed on create new bill</response>
        [HttpPost]
        public ActionResult<Bill> OpenBill(int tableNumber)
        {
            Table table = new() { Number = tableNumber };
            table = _tableService.GetByNumber(table);

            if (table == null)
            {
                return BadRequest($"No table with number {tableNumber} was found");
            }
            if (!table.Available)
            {
                return BadRequest($"Table {tableNumber} is not available");
            }

            Bill bill = new() { Table = table };
            bill = _billService.StartNew(bill);

            return Ok(bill);
        }

        /// <summary>
        /// Finish bill by table number
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <response code="200">Bill finished successfully</response>
        /// <response code="400">Has no bill to table number</response>
        [HttpPatch("{tableNumber}")]
        public ActionResult<Bill> CloseBill(int tableNumber)
        {
            Table table = new() { Number = tableNumber };
            table = _tableService.GetByNumber(table);

            if (table == null)
                return BadRequest($"Has no table with number {tableNumber}");

            Bill bill = new() { Table = table };

            bill = _billService.Close(bill);

            return bill != null ? Ok(bill) : BadRequest($"No bill to table {tableNumber} was found");
        }
    }
}
