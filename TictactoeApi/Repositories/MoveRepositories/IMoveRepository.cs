using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;

namespace TictactoeApi.Repositories.MoveRepositories
{
    public interface IMoveRepository
    {
        ICollection<Move> GetMoves();
        Move GetMove(int moveId);
        bool PlayerMove(Move move);
        bool DeleteMove(Move move);
        bool Save();
    }
}
