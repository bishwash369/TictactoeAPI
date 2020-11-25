using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int FirstPlayerId { get; set; }
        public int LastPLayerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<Player> Players { get; set; }

    }
}
