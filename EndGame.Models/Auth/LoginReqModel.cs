using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.Auth
{
    public class LoginReqModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
