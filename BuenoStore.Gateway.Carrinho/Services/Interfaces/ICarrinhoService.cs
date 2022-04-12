using BuenoStore.BuildingBlocks.Models;
using BuenoStore.Gateway.Api.Models;

namespace BuenoStore.Gateway.Api.Services.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoViewModel> ObterCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto);
    }
}
