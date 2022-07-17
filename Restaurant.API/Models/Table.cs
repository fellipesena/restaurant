using System.Collections.Generic;

namespace Restaurant.API.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Available { get; set; } = true;

        public IEnumerable<Bill> Orders { get; set; }
    }
}
