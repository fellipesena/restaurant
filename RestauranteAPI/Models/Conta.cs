using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestauranteAPI.Models
{
    public class Conta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Mesa")]
        [Column("id_mesa")]
        public int IdMesa { get; set; }
        public Mesa Mesa { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }
    }
}
