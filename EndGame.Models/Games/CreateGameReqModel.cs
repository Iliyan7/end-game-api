using EndGame.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.Games
{
    public class CreateGameReqModel
    {
        [Required]
        public string Title { get; set; }

        public IFormFile[] Images { get; set; }

        public int[] Genres { get; set; }

        public int[] Platforms { get; set; }

        public Game ToEntity()
        {
            return new Game
            {
                Title = this.Title,
            };
        }
    }
}
