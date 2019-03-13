using EndGame.Models;
using System.Threading.Tasks;

namespace EndGame.Services.Interfaces
{
    public interface IUsersService
    {
        Task<bool> RegisterAsync(RegisterRequestModel model);
    }
}
