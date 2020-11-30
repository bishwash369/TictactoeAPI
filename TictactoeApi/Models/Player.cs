using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        

        public ICollection<Game> Games { get; set; }
        public ICollection<Move> Moves { get; set; }
    }
}
