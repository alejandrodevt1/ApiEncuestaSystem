using ApiEncuestaSystem.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiEncuestaSystem.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosControllers:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public UsuariosControllers(ApplicationDbContext context , UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager , IConfiguration configuration) {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(
            CreedencialesUsuarioDTO creedencialesUser)
        {
            var user = new IdentityUser
            {
                Email = creedencialesUser.Email,
                UserName = creedencialesUser.Email
            };

            var EmailExist = await userManager.FindByEmailAsync(user.Email);
            if (EmailExist != null) 
            {
                return Conflict(new { message = "El email ya esta registrado." });
                
            }

            var resultado = await userManager.CreateAsync(user, creedencialesUser.Password);

            if (resultado.Succeeded)
            {
                return await BuilToken(user);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        private IEnumerable<IdentityError> ConstruirLoginIncorrecto()
        {
            var identityErrors = new IdentityError() { Description = "Login Incorrecto" };
            var errores = new List<IdentityError>();
            errores.Add(identityErrors);
            return errores;
        }


        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> login(CreedencialesUsuarioDTO login)
        {
            var usuario = await userManager.FindByEmailAsync(login.Email);
            if(usuario is null)
            {
                return BadRequest(new { message = "Login Incorrecto"});
            }

            var resultado = await signInManager.CheckPasswordSignInAsync(usuario,login.Password,
                lockoutOnFailure:false);

            if (resultado.Succeeded)
            {
                return await BuilToken(usuario);
            }
            else
            {
                var errores = ConstruirLoginIncorrecto();
                return BadRequest(errores);
            }
        }


        private async Task<RespuestaAutenticacionDTO> BuilToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("usuarioId", user.Id!),
                new Claim("email", user.Email!),
            };

            var claimsDb = await userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDb);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));
            var cred = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddDays(1);

            var tokenSecurity = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiracion,
                signingCredentials: cred);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);
            return new RespuestaAutenticacionDTO
            { Token = token,
              Expiracion = expiracion,
            };
        }


    }
}
