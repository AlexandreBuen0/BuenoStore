using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuenoStore.Carrinho.Api.Data.Configuration
{
    public class CarrinhoConfig : IEntityTypeConfiguration<Models.Carrinho>
    {
        public void Configure(EntityTypeBuilder<Models.Carrinho> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Itens)
                   .WithOne(i => i.Carrinho)
                   .HasForeignKey(c => c.CarrinhoId);
        }
    }
}
