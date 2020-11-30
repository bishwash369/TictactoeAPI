using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;

namespace TictactoeApi.Repositories.MoveRepositories
{
    public interface IMoveRepository
    {
        bool MoveExists(int id);
        ICollection<Move> GetMovesList();
        Move GetMove(int moveId);
        bool PlayerMove(Move move);
        bool DeleteMove(Move move);
        bool Save();
    }
}
