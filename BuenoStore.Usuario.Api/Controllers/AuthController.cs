using BuenoStore.Usuario.Api.Configuration;
using BuenoStore.Usuario.Api.Model.Token;
using BuenoStore.Usuario.Api.Model.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuenoStore.Usuario.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("registrar-usuario")]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioModel registrarUsuario)
        {
            if (!ModelState.IsValid) 
                return CustomResponse(ModelState);

            var identityUser = new IdentityUser
            {
                UserName = registrarUsuario.Email,
                Email = registrarUsuario.Email
            };

            var result = await _userManager.CreateAsync(identityUser, registrarUsuario.Senha);

            if (result.Succeeded)
                return CustomResponse(await GerarJwt(registrarUsuario.Email));

            foreach (var error in result.Errors)
                AdicionarErroProcessamento(error.Description);

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LogarModel logar)
        {
            if (!ModelState.IsValid) 
                return CustomResponse(ModelState);

            var isPersistent = false;
            var lockoutOnFailure = true;

            var result = await _signInManager.PasswordSignInAsync(logar.Email, logar.Senha, isPersistent, lockoutOnFailure);

            if (result.Succeeded)
                return CustomResponse(await GerarJwt(logar.Email));

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado devido a muitas tentativas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        private async Task<JwtToken> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var identityClaims = ObterClaimsUsuario(user);
            var encodedToken = CodificarToken(identityClaims);

            return ObterToken(encodedToken, user);
        }

        private ClaimsIdentity ObterClaimsUsuario(IdentityUser user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Chave);
            
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.SistemaEmissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private JwtToken ObterToken(string encodedToken, IdentityUser user)
        {
            return new JwtToken
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
