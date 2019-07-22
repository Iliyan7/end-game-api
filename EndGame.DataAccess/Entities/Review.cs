using System;
using System.ComponentModel.DataAnnotations;

namespace EndGame.DataAccess.Entities
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string Gameplay { get; set; }

        [Required]
        public string Conclusion { get; set; }

        [Range(0, 5)]
        public int PriceRating { get; set; }

        [Range(0, 5)]
        public int GraphicsRating { get; set; }

        [Range(0, 5)]
        public int LevelsRating { get; set; }

        [Range(0, 5)]
        public int DifficultyRating { get; set; }

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;

        public int ReviewedGameId { get; set; }
        public Game ReviewedGame { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
