using EndGame.Models.Auth;
using EndGame.Models.Users;
using EndGame.Services.Results;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndGame.Services.Contracts
{
    public interface IUsersService
    {
        Task<ServiceResult> CreateAsync(RegisterReqModel model);

        Task<ServiceResult<ClaimsIdentity>> PasswordSignInAsync(string email, string password);

        //void AddToSubscribersAsync(string email);
        Task<ServiceResult> AddToSubscribersAsync(string email);
    }
}
