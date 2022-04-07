using BuenoStore.Carrinho.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace BuenoStore.Carrinho.Api.Data
{
    public class CarrinhoContext : DbContext
    {
        public CarrinhoContext(DbContextOptions<CarrinhoContext> options)
            : base(options) { }

        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
        public DbSet<Model.Carrinho> Carrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarrinhoContext).Assembly);
        }
    }
}
