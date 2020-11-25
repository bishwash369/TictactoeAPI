using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Data;
using TictactoeApi.Models;
using TictactoeApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TictactoeApi.Repositories
{
    public class TictactoeRepository : ITictactoeRepository
    {
       private readonly ApplicationDbContext _db;

        public TictactoeRepository(ApplicationDbContext db)
        {
            _db = db;                                                       //to access dbcontext
        }
        public bool CreatePlayer(Player player)
        {
            _db.Players.Add(player);
            return Save();
        }

        public bool DeletePlayer(Player player)
        {
            _db.Players.Remove(player);
            return Save();
        }

        public Player GetPlayer(int playerId)
        {
            var data = _db.Players.FirstOrDefault(a => a.Id == playerId);
            return data;
        }

        public ICollection<Player> GetPlayers()
        {
            var data = _db.Players.OrderBy(a => a.Name).ToList();
            return data;
        }

        public bool PlayerExists(int id)
        {
            bool value = _db.Players.Any(a => a.Id == id);
            return value;
        }

        public bool PlayerExists(string name)
        {
            bool value = _db.Players.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdatePlayer(Player player)
        {
            _db.Players.Update(player);
            return Save();
        }
    }
}
