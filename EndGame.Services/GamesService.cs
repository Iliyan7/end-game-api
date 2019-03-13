using EndGame.DataAccess;
using EndGame.Services.Interfaces;

namespace EndGame.Services
{
    public class GamesService : IGamesService
    {
        private readonly EndGameContext _db;

        public GamesService(EndGameContext db)
        {
            this._db = db;
        }
    }
}
