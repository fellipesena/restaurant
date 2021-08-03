using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestauranteAPI.Models
{
    [Table("itens_pedido")]
    public class ItensPedido
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonIgnore]
        [Column("id_pedido")]
        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }
        [JsonIgnore]
        public Pedido Pedido { get; set; }

        [ForeignKey("Item")]
        [Column("id_item")]
        public int IdItem { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }
        public int Quantidade { get; set; }
        
        [JsonIgnore]
        [Column("valor_unitario")]
        public decimal ValorUnitario { get; set; }
        
        [JsonIgnore]
        [Column("valor_total")]
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
    }
}
