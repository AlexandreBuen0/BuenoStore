using BuenoStore.Carrinho.Api.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BuenoStore.Carrinho.Api.Data
{
    public class CarrinhoContext : DbContext
    {
        public CarrinhoContext(DbContextOptions<CarrinhoContext> options)
            : base(options) { }

        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
        public DbSet<Models.Carrinho> Carrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarrinhoContext).Assembly);
        }
    }
}
