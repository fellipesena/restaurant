using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Models
{
    public class Order
    {
        public Order(int waiterId, int billId, int tableId)
        {
            Value = 0;
            TableId = tableId;
            DateTime = DateTime.UtcNow;
            WaiterId = waiterId;
            BillId = billId;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Bill")]
        public int BillId { get; set; }
        public virtual Bill Bill { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        [ForeignKey("Waiter")]
        public int WaiterId { get; set; }
        public virtual Waiter Waiter { get; set; }
        public virtual IEnumerable<OrderItems> OrderItems { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }

        internal void IncreaseTotalValue(decimal totalValue)
        {
            Value += totalValue;
        }
    }
}
