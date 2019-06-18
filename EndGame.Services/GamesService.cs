using System;
using System.Linq;
using System.Threading.Tasks;
using EndGame.Constants;
using EndGame.Constants.ErrorMessages;
using EndGame.DataAccess;
using EndGame.DataAccess.Entities;
using EndGame.Models;
using EndGame.Models.Games;
using EndGame.Services.Contracts;
using EndGame.Services.Results;
using Microsoft.EntityFrameworkCore;

namespace EndGame.Services
{
    public class GamesService : BaseService, IGamesService
    {
        public GamesService(EndGameContext db) : base(db)
        {
        }

        private IQueryable<Game> Games => _db.Games;

        public async Task<Pagination<GameResModel>> GetAllAsync(string searchTerm, int pageIndex = 1)
        {
            var query = Games;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(e => EF.Functions.Like(e.Title, $"%{searchTerm}%"));
            }

            var totalPages = (int)Math.Ceiling(await query.CountAsync() / (double)Pagination.DefaultPageSize);

            query = CreatePaginatedResult(query, pageIndex, Pagination.DefaultPageSize);

            var result = await query
                .Select(g => new GameResModel
                {
                    Title = g.Title,
                })
                .ToListAsync();

            return new Pagination<GameResModel>()
            {
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Result = result
            };
        }

        public async Task<ServiceResult<GameResModel>> GetByIdAsync(int id)
        {
            var game = await Games.FirstOrDefaultAsync(e => e.Id == id);

            if (game == null)
            {
                return ServiceResult<GameResModel>.Failed(NotFound.StatusCode, new ResultError(NotFound.NoSuchGame));
            }

            var result = new GameResModel
            {
                Title = game.Title
            };

            return ServiceResult<GameResModel>.Success(result);
        }

        public async Task<ServiceResult<Game>> CreateAsync(CreateGameReqModel model)
        {
            var game = model.ToEntity();

            await _db.Games.AddAsync(game);
            await _db.SaveChangesAsync();

            return ServiceResult<Game>.Success(game);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateGameReqModel model)
        {
            if (!(await Games.AnyAsync(e => e.Id == id)))
            {
                return ServiceResult.Failed(NotFound.StatusCode, new ResultError(NotFound.NoSuchGame));
            }

            var gameToUpdate = model.ToEntity(id);
            var gameFilledProperties = GetFilledProperties(model);

            _db.Attach(gameToUpdate);

            foreach (var property in gameFilledProperties)
            {
                _db.Entry(gameToUpdate).Property(property).IsModified = true;
            }

            await _db.SaveChangesAsync();

            return ServiceResult.Success();
        }
    }
}
