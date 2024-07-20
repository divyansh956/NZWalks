using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        // API: api/auth/register
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            IdentityUser identityUser = new IdentityUser()
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            IdentityResult identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("Could not create user.");
        }

        [HttpPost]
        [Route("Login")]
        // API: api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            IdentityUser? identityUser = await userManager.FindByNameAsync(loginRequestDTO.Username);

            if (identityUser != null && await userManager.CheckPasswordAsync(identityUser, loginRequestDTO.Password))
            {
                IList<string> roles = await userManager.GetRolesAsync(identityUser);
                if (roles != null)
                {
                    string jwtToken = tokenRepository.CreateJWTToken(identityUser, roles.ToList());
                    return Ok(new LoginResponseDTO() { JwtToken = jwtToken });
                }
            }

            return BadRequest("Could not login user.");
        }
    }
}
