using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuenoStore.Pedido.Api.Data.Configuration
{
    public class PedidoConfig : IEntityTypeConfiguration<Models.Pedido>
    {
        public void Configure(EntityTypeBuilder<Models.Pedido> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Endereco, x =>
            {
                x.Property(x => x.Logradouro)
                    .HasColumnName("Logradouro");

                x.Property(x => x.Numero)
                    .HasColumnName("Numero");

                x.Property(x => x.Complemento)
                    .HasColumnName("Complemento");

                x.Property(x => x.Bairro)
                    .HasColumnName("Bairro");

                x.Property(x => x.Cep)
                    .HasColumnName("Cep");

                x.Property(x => x.Cidade)
                    .HasColumnName("Cidade");

                x.Property(x => x.Estado)
                    .HasColumnName("Estado");
            });

            builder.Property(x => x.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.HasMany(x => x.PedidoItems)
                   .WithOne(x => x.Pedido)
                   .HasForeignKey(x => x.PedidoId);

            builder.ToTable("Pedidos");
        }
    }
}
