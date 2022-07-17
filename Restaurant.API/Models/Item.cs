using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public int StockQuantity { get; set; }
    }
}
