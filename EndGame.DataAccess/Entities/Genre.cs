using System.Collections.Generic;

namespace EndGame.DataAccess.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<GameGenre> Games { get; set; } = new List<GameGenre>();
    }
}
