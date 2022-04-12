using BuenoStore.BuildingBlocks.Api.Usuario;
using BuenoStore.BuildingBlocks.Api.Usuario.Interface;
using BuenoStore.Carrinho.Api.Data;

namespace BuenoStore.Carrinho.Api.Configuration
{
    public static class InjecaoDependencia
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<CarrinhoContext>();
        }
    }
}