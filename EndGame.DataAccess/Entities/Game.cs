using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EndGame.DataAccess.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public IList<GameImage> Images { get; set; } = new List<GameImage>();
    }
}
