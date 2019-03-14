using System.ComponentModel.DataAnnotations;

namespace EndGame.DataAccess.Entities
{
    public class Subscriber
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
