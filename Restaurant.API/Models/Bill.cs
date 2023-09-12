using Restaurant.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Models
{
    public class Bill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int TableId { get; private set; }
        public virtual Table Table { get; private set; }
        public virtual IEnumerable<Order> Orders { get; private set; }
        public BillStatus Status { get; private set; }
        public decimal Value { get; private set; }

        internal void StartNew()
        {
            Status = BillStatus.Openned;
            Value = 0;
        }

        internal void Close()
        {
            Status = BillStatus.Closed;
            Table.Available = true;
        }

        internal void IncreaseTotalValue(decimal orderValue)
        {
            Value += orderValue;
        }
    }
}
