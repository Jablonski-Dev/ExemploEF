using ExemploEF.Models;
using Microsoft.EntityFrameworkCore;

namespace ExemploEF.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // Tabelas do Banco de Dados
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Categoria> Produto { get; set; }



        // Configurações das Entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Categoria>().ToTable("Produtos");

        }
        public DbSet<ExemploEF.Models.Produto> Produto_1 { get; set; } = default!;
    }
}

