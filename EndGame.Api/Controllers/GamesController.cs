using EndGame.Models.GameRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(CreateGameReqModel model)
        {
            return Ok();
        }
    }
}