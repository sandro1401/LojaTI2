using System.ComponentModel.DataAnnotations;

namespace LojaTI2.Models
{
    public class NotaFiscalModel
    {
        public int Id { get; set; }

        // Número da nota fiscal
        public string Numero { get; set; }

        // Data de emissão da nota fiscal
        public DateTime DataEmissao { get; set; }

        // Identificador do pedido associado à nota fiscal
        public int PedidoId { get; set; }

        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public ClienteModel Cliente { get; set; }

        public decimal ValorProdutos { get; set; }


        public decimal ValorTotal { get; set; }

        // Observações adicionais sobre a nota fiscal
        public string Observacoes { get; set; }

        public List<ItemPedidoModel> Itens { get; set; }
    }
}
