using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndGame.DataAccess.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Review Review { get; set; }

        public IList<GameImage> Images { get; set; } = new List<GameImage>();

        public IList<GameGenre> Genres { get; set; } = new List<GameGenre>();

        public IList<GamePlatform> Platforms { get; set; } = new List<GamePlatform>();
    }
}
