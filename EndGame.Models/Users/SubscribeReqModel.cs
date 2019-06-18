using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.Users
{
    public class SubscribeReqModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
