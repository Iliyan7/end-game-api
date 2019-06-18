using CryptoHelper;
using EndGame.Constants;
using EndGame.Constants.ErrorMessages;
using EndGame.DataAccess;
using EndGame.DataAccess.Entities;
using EndGame.Models.Auth;
using EndGame.Models.Users;
using EndGame.Services.Contracts;
using EndGame.Services.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndGame.Services
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(EndGameContext db) : base(db)
        {
        }

        private IQueryable<User> Users => _db.Users;

        public async Task<ServiceResult> CreateAsync(RegisterReqModel model)
        {
            if (await Users.AnyAsync(u => u.Email == model.Email))
            {
                return ServiceResult.Failed(BadRequest.StatusCode, new ResultError(BadRequest.EmailAlreadyExists));
            }

            var hashedPassword = Crypto.HashPassword(model.Password);
            var user = model.ToEntity(hashedPassword);

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<ClaimsIdentity>> PasswordSignInAsync(string email, string password)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return ServiceResult<ClaimsIdentity>.Failed(Unauthorized.StatusCode, new ResultError(Unauthorized.InvalidLogin));
            }

            var hashedPassword = Crypto.HashPassword(password);

            if (!Crypto.VerifyHashedPassword(hashedPassword, password))
            {
                return ServiceResult<ClaimsIdentity>.Failed(Unauthorized.StatusCode, new ResultError(Unauthorized.InvalidLogin));
            }

            return ServiceResult<ClaimsIdentity>.Success(this.GenerateClaims(user));
        }

        // TODO: should lock context
        public async Task<ServiceResult> AddToSubscribersAsync(string email)
        {
            if (await _db.Subscribers.AnyAsync(s => s.Email == email))
            {
                return ServiceResult.Failed(BadRequest.StatusCode, new ResultError(BadRequest.EmailAlreadySubscribed));
            }

            await _db.Subscribers.AddAsync(new Subscriber()
            {
                Email = email
            });

            await _db.SaveChangesAsync();

            return ServiceResult.Success();
        }

        private ClaimsIdentity GenerateClaims(User user)
        {
            var userId = user.Id.ToString();
            var email = user.Email;
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var roles = user.Roles.Select(r => r.Role.Name).ToList();

            var id = new ClaimsIdentity("EndGame.Application", EndGameClaimTypes.Email, EndGameClaimTypes.Role);
            id.AddClaim(new Claim(EndGameClaimTypes.Id, userId));
            id.AddClaim(new Claim(EndGameClaimTypes.Email, email));
            id.AddClaim(new Claim(EndGameClaimTypes.FirstName, firstName));
            id.AddClaim(new Claim(EndGameClaimTypes.LastName, lastName));
            roles.ForEach(role => id.AddClaim(new Claim(EndGameClaimTypes.Role, role)));
           
            return id;
        }
    }
}
