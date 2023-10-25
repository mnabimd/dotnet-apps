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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto body)
        {
            var identityUser = new IdentityUser()
            {
                UserName = body.UserName,
                Email = body.UserName,
            };

            var identityResult = await userManager.CreateAsync(identityUser, body.Password);

            if (identityResult.Succeeded)
            {
                // Add rules to this user
                if (body.Roles != null && body.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, body.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User is created. Login now");
                    }
                }
            }

            return BadRequest(identityResult.Errors);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto data)
        {
            var user = await userManager.FindByEmailAsync(data.UserName);

            // Why not (!user)

            if (user != null)
            {
                var passwordMatched = await userManager.CheckPasswordAsync(user, data.Password);

                if (passwordMatched)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        return Ok(new LoginResponse()
                        {
                            JwtToken = jwtToken
                        });
                    }

                }
            }

            return BadRequest("Incorrect password or username");

        }
    }
}
