using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteAPI.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Bill")]
        public int BillId { get; set; }
        public Bill Bill { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table Table { get; set; }

        [ForeignKey("Waiter")]
        public int WaiterId { get; set; }
        public Waiter Waiter { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
    }
}
