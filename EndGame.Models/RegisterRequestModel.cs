using EndGame.DataAccess.Entities;

namespace EndGame.Models
{
    public class RegisterRequestModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public User ToEntity(string passwordHash)
        {
            return new User()
            {
                Email = this.Email,
                PasswordHash = passwordHash,
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }
}
