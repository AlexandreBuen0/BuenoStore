using BuenoStore.Pedido.Api.Data;

namespace BuenoStore.Pedido.Api.Configuration
{
    public static class InjecaoDependencia
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Data
            services.AddScoped<PedidosContext>();
        }
    }
}