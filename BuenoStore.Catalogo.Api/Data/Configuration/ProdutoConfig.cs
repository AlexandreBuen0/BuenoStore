using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuenoStore.Catalogo.Api.Models;

namespace BuenoStore.Catalogo.Api.Data.Configuration
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Imagem)
                .IsRequired(false)
                .HasMaxLength(250);
        }
    }
}
