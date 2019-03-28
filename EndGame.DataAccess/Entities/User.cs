using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndGame.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => this.FirstName + " " + this.LastName;

        public string ProfilePic { get; set; }

        public IList<UserRole> Roles { get; set; } = new List<UserRole>();

        [ForeignKey("AuthorId")]
        public IList<Review> Reviews { get; set; } = new List<Review>();
    }
}
