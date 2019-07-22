using EndGame.DataAccess.Entities;

namespace EndGame.Models.Games
{
    public class UpdateGameReqModel
    {
        public string Title { get; set; }

        public Game ToEntity(int id)
        {
            return new Game()
            {
                Id = id,
                Title = this.Title,
            };
        }
    }
}
