using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;

namespace TictactoeApi.Repositories
{
    public interface ITictactoeRepository
    {
        ICollection<Player> GetPlayers();
        Player GetPlayer(int playerId);
        bool PlayerExists(int id);
        bool PlayerExists(string name);
        bool CreatePlayer(Player player);
        bool UpdatePlayer(Player player);
        bool DeletePlayer(Player player);
        bool Save();
    }
}
