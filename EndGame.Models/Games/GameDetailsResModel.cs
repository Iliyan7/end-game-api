using System.Collections.Generic;

namespace EndGame.Models.Games
{
    public class GameDetailsResModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<GenreInfo> Genres { get; set; }

        public IEnumerable<PlatformInfo> Platforms { get; set; }
    }

    public class GenreInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class PlatformInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
