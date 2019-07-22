using EndGame.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.Reviews
{
    public class CreateReviewReqModel
    {
        [Required]
        public string Gameplay { get; set; }

        [Required]
        public string Conclusion { get; set; }

        public int PriceRating { get; set; }

        public int GraphicsRating { get; set; }

        public int LevelsRating { get; set; }

        public int DifficultyRating { get; set; }

        public Review ToEntity()
        {
            return new Review
            {
                Gameplay = this.Gameplay,
                Conclusion = this.Conclusion,
                PriceRating = this.PriceRating,
                GraphicsRating = this.GraphicsRating,
                LevelsRating = this.LevelsRating,
                DifficultyRating = this.DifficultyRating,

            };
        }
    }
}
