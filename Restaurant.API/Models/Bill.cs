using Restaurant.API.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Models
{
    public class Bill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table Table { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
        public BillStatus Status { get; set; }
        public decimal Value { get; set; }
    }
}
