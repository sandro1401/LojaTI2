using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaTI2.Models
{
    [Table("Categoria")]
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }

      
        public List<ProdutoModel> Produtos { get; set; } = new List<ProdutoModel>();
    }
}
