using EndGame.Api.TokenProviders.Contracts;
using EndGame.Models.Auth;
using EndGame.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EndGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IUsersService usersService, ITokenProvider tokenProvider)
        {
            _usersService = usersService;
            _tokenProvider = tokenProvider;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterReqModel model)
        {
            var result = await _usersService.CreateAsync(model);

            if (!result.Succeeded)
            {
                return StatusCode(result.StatusCode, result.Errors);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginReqModel model)
        {
            var result = await _usersService.PasswordSignInAsync(model.Email, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(result.StatusCode, result.Errors);
            }

            var accessToken = _tokenProvider.GenerateToken(result.Data.Claims);

            return Ok(new { accessToken });
        }
    }
}
