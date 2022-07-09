using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Context.Core;
using Restaurant.API.Models;
using System.Linq;

namespace Restaurant.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BillController : Controller
    {
        private readonly IUnitOfWork _uow;

        public BillController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Get bill by number of table
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <response code="200">Bill by number of table</response>
        /// <response code="400">Table has no bill openned</response>
        [HttpGet("{tableNumber}")]
        public ActionResult<Bill> GetBill(int tableNumber)
        {
            Bill bill = _uow.Bills.GetBillByTableNumber(tableNumber);

            return bill == null ? BadRequest("Table has no one bill openned") : bill;
        }

        /// <summary>
        /// Create new bill
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <response code="200">Bill created successfully</response>
        /// <response code="400">Failed on create new bill</response>
        [HttpPost]
        public ActionResult<Bill> PostConta(int tableNumber)
        {
            Table table = _uow.Tables.Find(_table => _table.Number == tableNumber).FirstOrDefault();

            if (table == null)
            {
                return BadRequest($"No table with number {tableNumber} was found");
            }
            if (!table.Available)
            {
                return BadRequest($"Table {tableNumber} is not available");
            }

            Bill bill = new()
            {
                Status = Enums.BillStatus.Openned,
                TableId = table.Id,
                Value = 0
            };

            _uow.Bills.Add(bill);

            table.Available = false;

            _uow.Complete();

            return bill;
        }

        /// <summary>
        /// Finish bill by table number
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <response code="200">Bill finished successfully</response>
        /// <response code="400">Has no bill to table number</response>
        [HttpPut("{tableNumber}")]
        public ActionResult<Bill> PutConta(int tableNumber)
        {
            Bill bill = _uow.Bills.GetBillByTableNumber(tableNumber);

            if (bill == null)
            {
                return NotFound($"No bill to table {tableNumber} was found");
            }

            Table table = _uow.Tables.Get(bill.TableId);

            table.Available = true;
            bill.Status = Enums.BillStatus.Closed;

            _uow.Complete();

            return bill;
        }
    }
}
