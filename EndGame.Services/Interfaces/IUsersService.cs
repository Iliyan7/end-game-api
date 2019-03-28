using EndGame.Models.UserRequests;
using EndGame.Services.Results;
using System.Threading.Tasks;

namespace EndGame.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ServiceResult> CreateAsync(RegisterReqModel model);

        Task<ServiceResult> PasswordSignInAsync(string email, string password);

        void AddToSubscribers(SubscribeReqModel model);
    }
}
