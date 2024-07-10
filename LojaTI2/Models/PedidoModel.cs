using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaTI2.Models
{
    [Table("Pedido")]
    public class PedidoModel
    {
        
        public int Id { get; set; }

        public DateTime? DataPedido { get; set; }

        public DateTime? DataEntrega { get; set; }

        public double? ValorTotal { get; set; }

        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public ClienteModel Cliente { get; set; }

        public EnderecoModel EnderecoEntrega { get; set; }

        public List<ItemPedidoModel> ItensPedido { get; set; }
    }
}
