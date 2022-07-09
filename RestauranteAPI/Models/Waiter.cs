using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteAPI.Models
{
    public class Waiter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
