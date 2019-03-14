using CryptoHelper;
using EndGame.DataAccess;
using EndGame.DataAccess.Entities;
using EndGame.Models;
using EndGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EndGame.Services
{
    public class UsersService : IUsersService
    {
        private readonly EndGameContext _db;

        public UsersService(EndGameContext db)
        {
            this._db = db;
        }

        public DbSet<Subscriber> Subscribers => _db.Subscribers;

        public DbSet<User> Users => _db.Users;

        public async Task<bool> CreateAsync(RegisterRequestModel model)
        {
            if(await Users.AnyAsync(u => u.Email == model.Email))
            {
                return false;
            }

            var hashedPassword = Crypto.HashPassword(model.Password);
            var entity = model.ToEntity(hashedPassword);

            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PasswordSignInAsync(string email, string password)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if(user == null)
            {
                return false;
            }

            var hashedPassword = Crypto.HashPassword(password);

            if(!user.PasswordHash.Equals(hashedPassword))
            {
                return false;
            }

            return true;
        }

        public async void AddToSubscribers(SubscribeRequestModel model)
        {
            if (await Subscribers.AnyAsync(s => s.Email == model.Email))
            {
                return;
            }

            await _db.Subscribers.AddAsync(new Subscriber()
            {
                Email = model.Email
            });

            await _db.SaveChangesAsync();
        }
    }
}
