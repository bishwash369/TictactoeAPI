using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string MoveType { get; set; }
        public int Position {get; set;}
        public int PlayerId { get; set; }
        public int GameId { get; set; }
                
        public virtual Player Players { get; set; }
        public virtual Game Games { get; set; }

    }
}
