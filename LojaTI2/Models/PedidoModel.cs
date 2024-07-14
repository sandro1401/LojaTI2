using EstoqueWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaTI2.Models
{
    [Table("Pedido")]
    public class PedidoModel : UsuarioModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime? DataPedido { get; set; }

        public DateTime? DataEntrega { get; set; }

        public double? ValorTotal { get; set; }

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public ClienteModel Cliente { get; set; }

        
      

        public ICollection<ItemPedidoModel>? ItensPedido { get; set; }

       
    }
}
