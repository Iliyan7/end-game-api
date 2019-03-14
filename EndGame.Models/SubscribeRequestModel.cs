using System.ComponentModel.DataAnnotations;

namespace EndGame.Models
{
    public class SubscribeRequestModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
