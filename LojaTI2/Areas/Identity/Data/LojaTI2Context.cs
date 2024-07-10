using LojaTI2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LojaTI2.Models;

namespace LojaTI2.Data;

public class LojaTI2Context : IdentityDbContext<LojaTI2User>
{
    public LojaTI2Context(DbContextOptions<LojaTI2Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<LojaTI2.Models.EnderecoModel> EnderecoModel { get; set; } = default!;

public DbSet<LojaTI2.Models.ClienteModel> ClienteModel { get; set; } = default!;

public DbSet<LojaTI2.Models.ItemPedidoModel> ItemPedidoModel { get; set; } = default!;

public DbSet<LojaTI2.Models.ProdutoModel> ProdutoModel { get; set; } = default!;

public DbSet<LojaTI2.Models.PedidoModel> PedidoModel { get; set; } = default!;

public DbSet<LojaTI2.Models.NotaFiscalModel> NotaFiscalModel { get; set; } = default!;

public DbSet<LojaTI2.Models.CategoriaModel> CategoriaModel { get; set; } = default!;
}
