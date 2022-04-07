using BuenoStore.BuildingBlocks.Api.Usuario.Interface;
using Microsoft.AspNetCore.Http;

namespace BuenoStore.BuildingBlocks.Api.Usuario
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid ObterUsuarioId() => EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.ObterUsuarioId()) : Guid.Empty;

        public string ObterUsuarioEmail() => EstaAutenticado() ? _accessor.HttpContext.User.ObterUsuarioEmail() : "";

        public string ObterUsuarioToken() => EstaAutenticado() ? _accessor.HttpContext.User.ObterTokenUsuario() : "";

        public bool EstaAutenticado() =>_accessor.HttpContext.User.Identity.IsAuthenticated;

        public HttpContext ObterHttpContext() => _accessor.HttpContext;
    }
}
