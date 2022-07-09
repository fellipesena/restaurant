using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Models
{
    public class Waiter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
