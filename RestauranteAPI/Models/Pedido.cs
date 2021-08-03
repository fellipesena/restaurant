using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestauranteAPI.Models
{
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Conta")]
        [Column("id_conta")]
        public int IdConta { get; set; }
        public Conta Conta { get; set; }

        [ForeignKey("Mesa")]
        [Column("id_mesa")]
        public int IdMesa { get; set; }
        public Mesa Mesa { get; set; }

        [ForeignKey("Garcom")]
        [Column("id_garcom")]
        public int IdGarcom { get; set; }
        public Garcom Garcom { get; set; }
        public IEnumerable<ItensPedido> ItensPedido { get; set; }
        public DateTime Horario { get; set; }
        public decimal Valor { get; set; }
    }
}
