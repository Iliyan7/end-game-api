using EndGame.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.Games
{
    public class CreateGameReqModel
    {
        [Required]
        public string Title { get; set; }

        public Game ToEntity()
        {
            return new Game
            {
                Title = this.Title,
            };
        }
    }
}
