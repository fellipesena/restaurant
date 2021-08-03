using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestauranteAPI.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public bool Disponivel { get; set; }
        [JsonIgnore]
        public IEnumerable<Conta> Contas { get; set; }
    }
}
