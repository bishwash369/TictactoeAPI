using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;

namespace TictactoeApi.Repositories.GameRepositories
{
    public interface IGameRepository

    {
        ICollection<Game> GetGames();
        Player GetGame(int gameId);
        bool StartGame(Game game);
        bool GameExists(int id);
        bool UpdateGame(Game game);
       // bool DeleteGame(Game game);
        bool Save();
    }
}
