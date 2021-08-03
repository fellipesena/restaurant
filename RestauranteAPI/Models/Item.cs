using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }

        [Column("qtd_estoque")]
        public int QtdEstoque { get; set; }
    }
}
