using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaTI2.Models
{
    [Table("Cliente")]
    public class ClienteModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required]
        [MinLength(2), MaxLength(30)]
        public string Nome { get; set; }

        public string Email { get; set; }

        [Required, Column(TypeName = "char(14)")]
        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        [NotMapped]
        public int Idade
        {
            get => (int)Math.Floor((DateTime.Now - DataNascimento).TotalDays / 365.2425);
        }
       

        public ICollection<EnderecoModel> Enderecos { get; set; } = new List<EnderecoModel>();

        public ICollection<PedidoModel> Pedidos { get; set; }



    }
}

