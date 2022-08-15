using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApAutores.DTOs;

namespace WebApAutores.Controllers
{
    [ApiController]
    [Route("api/Cuentas")]
    public class CuentasController2 : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasController2(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registrar", Name = "registrarUsuario")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuarios credencialesUsuarios)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuarios.Email, Email=credencialesUsuarios.Email};
            var resultado = await userManager.CreateAsync(usuario,credencialesUsuarios.Password);

            if (resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuarios);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

        }

        [HttpPost("loguin", Name = "loguinUsuario")]
        public async Task<ActionResult<RespuestaAutenticacion>> Loguin(CredencialesUsuarios credencialesUsuarios)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuarios.Email, credencialesUsuarios.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuarios);
            }
            else
            {
                return BadRequest("loguin incorrecto ");
            }
        }


        private RespuestaAutenticacion ConstruirToken(CredencialesUsuarios credencialesUsuarios)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuarios.Email),
                new Claim("nuevo clim","lo otro que quieras asignar ")
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience:null,claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }

    }
}
