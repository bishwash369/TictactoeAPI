using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Data;
using TictactoeApi.Models;

namespace TictactoeApi.Repositories.MoveRepositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly ApplicationDbContext _db;

        public MoveRepository(ApplicationDbContext db)
        {
            _db = db;

        }


        public bool DeleteMove(Move move)
        {
            _db.Moves.Remove(move);
            return Save();
        }

        public Move GetMove(int moveId)
        {
            var data = _db.Moves.FirstOrDefault(a => a.Id == moveId);
            return data;
        }

        public ICollection<Move> GetMoves()
        {
            var data = _db.Moves.OrderBy(a => a.Id).ToList();
            return (ICollection<Move>)data;
        }

        public bool PlayerMove(Move move)
        {
            _db.Moves.Add(move);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }
    }
}
