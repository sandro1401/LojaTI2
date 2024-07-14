using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace LojaTI2.Models
{
    [Table("ItemPedido")]
    public class ItemPedidoModel
    { 
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPedido { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProduto { get; set; }

        public int Quantidade { get; set; }

        public double ValorUnitario { get; set; }

        [ForeignKey("PedidoId")]
        public PedidoModel Pedido { get; set; }

        [ForeignKey("ProdutoId")]
        public ProdutoModel Produto { get; set; }

      

        [NotMapped]
        public double ValorItem { get => this.Quantidade * this.ValorUnitario; }
    }
}
