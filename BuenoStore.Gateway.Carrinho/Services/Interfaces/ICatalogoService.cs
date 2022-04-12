using BuenoStore.Gateway.Api.Models;

namespace BuenoStore.Gateway.Api.Services.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterItens();
        Task<ProdutoViewModel> ObterPorId(Guid id);
    }
}
