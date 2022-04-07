using BuenoStore.Carrinho.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuenoStore.Carrinho.Api.Data.Configuration
{
    public class CarrinhoItemConfig : IEntityTypeConfiguration<CarrinhoItem>
    {
        public void Configure(EntityTypeBuilder<CarrinhoItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
