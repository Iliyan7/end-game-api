using System;
using System.Collections.Generic;
using System.Text;

namespace EndGame.DataAccess.Entities
{
    public class Platform
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<GamePlatform> Platforms { get; set; } = new List<GamePlatform>();
    }
}
