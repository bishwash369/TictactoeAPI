using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Data;
using TictactoeApi.Models;
using TictactoeApi.Repositories.GameRepositories;

namespace TictactoeApi.Repositories.GameRepositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _db;

        public GameRepository(ApplicationDbContext db)
        {
            _db = db;                                                       //to access dbcontext
        }

        public bool CreateGame(Game game)
        {
            _db.Games.Add(game);
            return Save();
        }

        public bool DeleteGame(Game game)
        {
            _db.Games.Remove(game);
            return Save();
        }

        public Game GetGame(int gameId)
        {
            var data = _db.Games.FirstOrDefault(a => a.Id == gameId);
            return data;
        }

        public ICollection<Game> GetGames()
        {
            var data = _db.Games.OrderBy(a => a.Id).ToList();
            return (ICollection<Game>)data;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateGame(Game game)
        {
            _db.Games.Update(game);
            return Save();
        }

        Player IGameRepository.GetGame(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
