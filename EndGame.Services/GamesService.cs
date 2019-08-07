using EndGame.Constants;
using EndGame.Constants.ErrorMessages;
using EndGame.DataAccess;
using EndGame.DataAccess.Entities;
using EndGame.Models;
using EndGame.Models.Games;
using EndGame.Services.Contracts;
using EndGame.Services.Results;
using EndGame.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EndGame.Services
{
    public class GamesService : BaseService, IGamesService
    {
        private readonly LocalStorage _storageProvider;

        public GamesService(EndGameContext db, LocalStorage storageProvider) : base(db)
        {
            this._storageProvider = storageProvider;
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
                    Id = g.Id,
                    Title = g.Title,
                    ThumbnailUrl = _storageProvider.GetStaticPath(g.Images.First().Path)
                })
                .ToListAsync();

            return new Pagination<GameResModel>()
            {
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Result = result
            };
        }

        public async Task<ServiceResult<GameDetailsResModel>> GetByIdAsync(int id)
        {
            var game = await Games
                .Select(g => new GameDetailsResModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Images = g.Images.Select(i => _storageProvider.GetStaticPath(i.Path)),
                    Genres = g.Genres.Select(gi => new GenreInfo
                    {
                        Id = gi.Genre.Id,
                        Name = gi.Genre.Name
                    }),
                    Platforms = g.Platforms.Select(p => new PlatformInfo
                    {
                        Id = p.Platform.Id,
                        Name = p.Platform.Name
                    })
                })
                .FirstOrDefaultAsync(e => e.Id == id);

            if (game == null)
            {
                return ServiceResult<GameDetailsResModel>.Failed(NotFound.StatusCode, new ResultError(NotFound.NoSuchGame));
            }

            return ServiceResult<GameDetailsResModel>.Success(game);
        }

        public async Task<ServiceResult<Game>> CreateAsync(CreateGameReqModel model)
        {
            if (!await AreGenresExists(model.Genres))
            {
                return ServiceResult<Game>.Failed(BadRequest.StatusCode, new ResultError(BadRequest.InvalidGenres));
            }

            if (!await ArePlatfromsExists(model.Platforms))
            {
                return ServiceResult<Game>.Failed(BadRequest.StatusCode, new ResultError(BadRequest.InvalidPlatfroms));
            }

            var game = model.ToEntity();

            foreach (var image in model.Images)
            {
                if (image.Length > 0)
                {
                    var keyName = GenerateUniqueFilename(Path.GetExtension(image.FileName));
                    await _storageProvider.UploadAsync(image.OpenReadStream(), keyName);

                    game.Images.Add(new GameImage()
                    {
                        Path = keyName,
                        Game = game
                    });
                }
            }

            foreach (var genreId in model.Genres)
            {
                game.Genres.Add(new GameGenre()
                {
                    Game = game,
                    GenreId = genreId
                });
            }

            foreach (var platfromId in model.Platforms)
            {
                game.Platforms.Add(new GamePlatform()
                {
                    Game = game,
                    PlatformId = platfromId
                });
            }

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

            UpdateEntry(gameToUpdate, gameFilledProperties);
            await _db.SaveChangesAsync();

            return ServiceResult.Success();
        }

        private async Task<bool> AreGenresExists(int[] genres)
        {
            foreach (var genreId in genres)
            {
                if (!await _db.Genres.AnyAsync(g => g.Id == genreId))
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> ArePlatfromsExists(int[] platfroms)
        {
            foreach (var platfromId in platfroms)
            {
                if (!await _db.Platforms.AnyAsync(p => p.Id == platfromId))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
