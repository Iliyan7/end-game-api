using EndGame.Models.UserRequests;
using EndGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("subscribe")]
        public ActionResult Subscribe(SubscribeReqModel model)
        {
            _usersService.AddToSubscribers(model);

            return Accepted();
        }
    }
}
