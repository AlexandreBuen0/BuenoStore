using BuenoStore.Catalogo.Api.Data;
using BuenoStore.Catalogo.Api.Data.Repository;
using BuenoStore.Catalogo.Api.Models.Interfaces;

namespace BuenoStore.Catalogo.Api.Configuration
{
    public static class InjecaoDependencia
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}
