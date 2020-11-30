using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models
{
    public class Move
    {
        public int Id { get; set; }
        public int MoveType { get; set; }
        public string Position {get; set;}
        public int PlayerId { get; set; }
        public int GameId { get; set; }
                
        public virtual Player Players { get; set; }
        public virtual Game Games { get; set; }

    }
}
