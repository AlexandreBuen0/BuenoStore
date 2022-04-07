using BuenoStore.BuildingBlocks.Data;
using BuenoStore.Pedido.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuenoStore.Pedido.Api.Data
{
    public class PedidosContext : DbContext, IUnitOfWork
    {
        public PedidosContext(DbContextOptions<PedidosContext> options)
            : base(options) { }

        public DbSet<Models.Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosContext).Assembly);

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
    }
}
