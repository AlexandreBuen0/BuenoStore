using BuenoStore.BuildingBlocks.Models;
using BuenoStore.Gateway.Api.Models;
using BuenoStore.Gateway.Api.Services.Interfaces;
using BuenoStore.Gateway.Carrinho.Configuration;
using Microsoft.Extensions.Options;

namespace BuenoStore.Gateway.Api.Services
{
    public class CarrinhoService : ServiceBase, ICarrinhoService
    {
        private readonly HttpClient _httpClient;

        public CarrinhoService(HttpClient httpClient, IOptions<ServiceSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/carrinho/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CarrinhoViewModel>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto)
        {
            var itemContent = ObterConteudo(produto);

            var response = await _httpClient.PostAsync("/carrinho/", itemContent);

            if (!TratarErrosResponse(response)) 
                return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
