﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LojaTI2.Models
{
    [Table("Produto")]
    public class ProdutoModel
    {
        
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }

        public int Estoque { get; set; }

        public double Preco { get; set; }

       
        [Display(Name ="Categoria")]
        public int CategoriaId { get; set; }
     
        public CategoriaModel Categoria { get; set; }
      
    }
}
