using System.ComponentModel.DataAnnotations;

namespace EndGame.DataAccess.Entities
{
    public class GameImage
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}