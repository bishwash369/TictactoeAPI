using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FirstPlayerId { get; set; }
        public int LastPlayerId { get; set; }

        public virtual Player FirstPlayer { get; set; }
        public virtual Player LastPlayer { get; set; }

        public ICollection<Move> Moves { get; set; }
    }
}
