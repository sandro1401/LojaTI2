using Microsoft.EntityFrameworkCore;
using LojaTI2.Models;

namespace LojaTI2.Models
{
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options)
            : base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ItemPedidoModel> ItensPedidos { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<NotaFiscalModel> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteModel>()
                .HasMany(c => c.Enderecos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.ClienteModelId);

            modelBuilder.Entity<PedidoModel>()
               
                    .ToTable("Pedido")
                ;
              
            modelBuilder.Entity<ItemPedidoModel>()
                .HasKey(ip => new { ip.IdPedido, ip.IdProduto });

            modelBuilder.Entity<CategoriaModel>().ToTable("Categoria");
            modelBuilder.Entity<ProdutoModel>().ToTable("Produto");
            modelBuilder.Entity<NotaFiscalModel>().ToTable("NotaFiscal");
        }

    }

}
