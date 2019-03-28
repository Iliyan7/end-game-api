using CryptoHelper;
using EndGame.DataAccess.Entities;
using System;
using System.Linq;

namespace EndGame.DataAccess
{
    public static class DbInitializer
    {
        private static readonly string[] defaultRoles = new string[] { "Admin", "Reviewer" };

        private static readonly string defaultAdminEmail = "admin@gmail.com";
        private static readonly string defaultAdminPassword = "123456";

        public static void Initialize(EndGameContext context)
        {
            SeedRoles(context);

            SeedAdminUser(context);
        }

        private static void SeedRoles(EndGameContext context)
        {
            foreach (var role in defaultRoles)
            {
                var roleExists = context.Roles.Any(r => r.Name.Equals(role));

                if (!roleExists)
                {
                    context.Roles.Add(new Role { Name = role });
                }
            }

            context.SaveChanges();
        }

        private static void SeedAdminUser(EndGameContext context)
        {
            var adminUser = context.Users.FirstOrDefault(u => u.Email.Equals(defaultAdminEmail));

            if (adminUser != null)
            {
                return;
            }

            adminUser = new User()
            {
                Email = defaultAdminEmail,
                PasswordHash = Crypto.HashPassword(defaultAdminPassword),
                FirstName = "Iliyan",
                LastName = "Ivanov",
            };

            context.Users.Add(adminUser);

            var adminRoleId = context.Roles.FirstOrDefault(u => u.Name.Equals("Admin")).Id;

            context.UserRoles.Add(new UserRole()
            {
                User = adminUser,
                RoleId = adminRoleId,
            });

            context.SaveChanges();
        }
    }
}
