using EndGame.Models.Users;
using EndGame.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("subscribe")]
        public ActionResult Subscribe(SubscribeReqModel model)
        {
            _usersService.AddToSubscribersAsync(model.Email);

            return Accepted();
        }
    }
}
