using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
                if (body.Roles != null && body.Roles.Any()) { 
                    identityResult = await userManager.AddToRolesAsync(identityUser, body.Roles);

                    if (identityResult.Succeeded) {
                        return Ok("User is created. Login now");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }
    }
}
