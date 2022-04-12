using BuenoStore.Gateway.Api.Models;
using BuenoStore.Gateway.Api.Services.Interfaces;
using BuenoStore.Gateway.Carrinho.Configuration;
using Microsoft.Extensions.Options;

namespace BuenoStore.Gateway.Api.Services
{
    public class CatalogoService : ServiceBase, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<ServiceSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterItens()
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }
    }
}
