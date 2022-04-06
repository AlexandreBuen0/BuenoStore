namespace BuenoStore.Usuario.Api.Model.Token
{
    public class JwtToken
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }
}
