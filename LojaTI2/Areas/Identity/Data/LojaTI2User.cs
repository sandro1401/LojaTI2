using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LojaTI2.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LojaTI2User class
public class LojaTI2User : IdentityUser
{
    [MaxLength(50, ErrorMessage = "O tamanho Máximo do campo é de {1} caracteres")]
    [Required]
    public string Nome { get; set; }

    [MaxLength(20, ErrorMessage = "O tamanho Máximo do campo é de {1} caracteres")]
    public string Telefone { get; set; }
}



