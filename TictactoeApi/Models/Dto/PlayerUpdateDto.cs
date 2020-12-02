using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models.Dto
{
    public class PlayerUpdateDto: PlayerDto
    {
        public int Id { get; set; }
    }
}
