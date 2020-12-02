using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models.Dto
{
    public class MoveDto
    {
       /* public MoveDto()
        {
            Games = new List<GameDto>();
        } */
        //public int Id { get; set; }
        public string MoveType { get; set; }
        public int Position { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        //public List<GameDto> Games { get; set; }
    }
}
