using Microsoft.AspNetCore.Http;

namespace BuenoStore.BuildingBlocks.Api.Usuario.Interface
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid ObterUsuarioId();
        string ObterUsuarioEmail();
        string ObterUsuarioToken();
        bool EstaAutenticado();
        HttpContext ObterHttpContext();
    }
}
