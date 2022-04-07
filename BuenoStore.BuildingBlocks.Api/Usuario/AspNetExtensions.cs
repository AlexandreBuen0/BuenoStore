using System.Security.Claims;

namespace BuenoStore.BuildingBlocks.Api.Usuario
{
    public static class AspNetExtensions
    {
        public static string ObterUsuarioId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string ObterUsuarioEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string ObterTokenUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}
