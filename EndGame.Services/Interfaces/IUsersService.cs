using EndGame.Models;
using System.Threading.Tasks;

namespace EndGame.Services.Interfaces
{
    public interface IUsersService
    {
        Task<bool> CreateAsync(RegisterRequestModel model);
        Task<bool> PasswordSignInAsync(string email, string password);

        void AddToSubscribers(SubscribeRequestModel model);
    }
}
