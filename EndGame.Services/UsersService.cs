using CryptoHelper;
using EndGame.DataAccess;
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

        public async Task<bool> RegisterAsync(RegisterRequestModel model)
        {
            if(await _db.Users.AnyAsync(u => u.Email == model.Email))
            {
                return false;
            }

            var hashedPassword = Crypto.HashPassword(model.Password);
            var entity = model.ToEntity(hashedPassword);

            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
