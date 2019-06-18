using EndGame.DataAccess.Entities;
using EndGame.Models;
using EndGame.Models.Games;
using EndGame.Services.Results;
using System.Threading.Tasks;

namespace EndGame.Services.Contracts
{
    public interface IGamesService
    {
        Task<Pagination<GameResModel>> GetAllAsync(string searchTerm, int pageIndex = 1);

        Task<ServiceResult<GameResModel>> GetByIdAsync(int id);

        Task<ServiceResult<Game>> CreateAsync(CreateGameReqModel model);

        Task<ServiceResult> UpdateAsync(int id, UpdateGameReqModel model);
    }
}
