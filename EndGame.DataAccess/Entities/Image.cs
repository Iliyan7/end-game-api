using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndGame.DataAccess.Entities
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        public IList<GameImage> Games { get; set; } = new List<GameImage>();
    }
}