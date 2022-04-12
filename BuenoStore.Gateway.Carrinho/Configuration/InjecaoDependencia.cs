using BuenoStore.BuildingBlocks.Api.Polly;
using BuenoStore.BuildingBlocks.Api.Usuario;
using BuenoStore.BuildingBlocks.Api.Usuario.Interface;
using BuenoStore.Gateway.Api.Authorization;
using BuenoStore.Gateway.Api.Services;
using BuenoStore.Gateway.Api.Services.Interfaces;
using Polly;

namespace NSE.Bff.Compras.Configuration.Configuration
{
    public static class InjecaoDependencia
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.PoliticaDeRetentativa())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICarrinhoService, CarrinhoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.PoliticaDeRetentativa())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }
    }
}