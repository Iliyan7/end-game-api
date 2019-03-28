using EndGame.Api.TokenProviders.Contracts;
using EndGame.DataAccess.Entities;
using EndGame.Models.UserRequests;
using EndGame.Services.Interfaces;
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

            if(!result.Succeeded)
            {
                return BadRequest("This Email already exists.");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginReqModel model)
        {
            var result = await _usersService.PasswordSignInAsync(model.Email, model.Password);

            if (!result.Succeeded)
            {
                return Unauthorized("Login attemp failed.");
            }

            var user = (User)result.Data;

            var accessToken = _tokenProvider.GenerateToken(user.Id, user.Email, new string[] { "Admin" });

            return Ok(new { accessToken });
        }
    }
}
