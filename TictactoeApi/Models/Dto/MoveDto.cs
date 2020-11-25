using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models.Dto
{
    public class MoveDto
    {
        public int Id { get; set; }
        public int MoveType { get; set; }
        public string Position { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Game Game { get; set; }
    }
}
