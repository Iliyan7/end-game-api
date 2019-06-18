using EndGame.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.Auth
{
    public class RegisterReqModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        public User ToEntity(string passwordHash)
        {
            return new User()
            {
                Email = this.Email,
                FirstName = FirstName,
                LastName = LastName,
                PasswordHash = passwordHash,
            };
        }
    }
}
