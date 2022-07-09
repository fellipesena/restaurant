using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestauranteAPI.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Available{ get; set; }

        [JsonIgnore]
        public IEnumerable<Bill> Orders { get; set; }
    }
}
