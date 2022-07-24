using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Models
{
    public class OrderItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int Quantity { get; set; }

        public decimal UnitValue { get; set; }

        public decimal TotalValue { get; set; }
        public string Comments { get; set; }
    }
}
