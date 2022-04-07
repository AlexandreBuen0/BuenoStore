using BuenoStore.Pedido.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuenoStore.Pedido.Api.Data.Configuration
{
    public class PedidoItemConfig : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProdutoNome)
                   .IsRequired()
                   .HasColumnType("varchar(250)");

            builder.HasOne(x => x.Pedido)
                   .WithMany(x => x.PedidoItems);

            builder.ToTable("PedidoItems");
        }
    }
}
