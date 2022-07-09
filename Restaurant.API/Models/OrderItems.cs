using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Restaurant.API.Models
{
    public class OrderItems
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonIgnore]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public decimal UnitValue { get; set; }

        [JsonIgnore]
        public decimal TotalValue { get; set; }
        public string Comments { get; set; }
    }
}
