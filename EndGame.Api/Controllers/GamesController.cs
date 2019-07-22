using EndGame.Models;
using EndGame.Models.Games;
using EndGame.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EndGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<object>>> GetAll(string searchTerm, int pageIndex = 1)
        {
            var result = await _gamesService.GetAllAsync(searchTerm, pageIndex);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<object>> GetById(int id)
        {
            var result = await _gamesService.GetByIdAsync(id);

            if (!result.Succeeded)
            {
                return StatusCode(result.StatusCode, result.Errors);
            }

            return result.Data;
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPost]
        public async Task<ActionResult<object>> Create([FromForm]CreateGameReqModel model)
        {
            var result = await _gamesService.CreateAsync(model);

            if (!result.Succeeded)
            {
                return StatusCode(result.StatusCode, result.Errors);
            }

            return Ok(result.Data);
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPatch]
        public async Task<ActionResult> Update(int id, UpdateGameReqModel model)
        {
            var result = await _gamesService.UpdateAsync(id, model);

            if (!result.Succeeded)
            {
                return StatusCode(result.StatusCode, result.Errors);
            }

            return NoContent();
        }
    }
}