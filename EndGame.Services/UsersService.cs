using CryptoHelper;
using EndGame.DataAccess;
using EndGame.DataAccess.Entities;
using EndGame.Models.UserRequests;
using EndGame.Services.Interfaces;
using EndGame.Services.Results;
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

        public async Task<ServiceResult> CreateAsync(RegisterReqModel model)
        {
            if (await _db.Users.AnyAsync(u => u.Email == model.Email))
            {
                return ServiceResult.Failed();
            }

            var hashedPassword = Crypto.HashPassword(model.Password);
            var user = model.ToEntity(hashedPassword);

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return ServiceResult.Success(user);
        }

        public async Task<ServiceResult> PasswordSignInAsync(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user == null)
            {
                return ServiceResult.Failed();
            }

            var hashedPassword = Crypto.HashPassword(password);

            if (!Crypto.VerifyHashedPassword(hashedPassword, password))
            {
                return ServiceResult.Failed();
            }

            return ServiceResult.Success(user);
        }

        // TODO: should lock context
        public async void AddToSubscribers(SubscribeReqModel model)
        {
            if (await _db.Subscribers.AnyAsync(s => s.Email == model.Email))
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
