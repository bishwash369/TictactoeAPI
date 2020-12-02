using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Controllers.Dto
{
    public class UpdatePlayerDto:CreatePlayersDto
    {
        public int Id { get; set; }
    }
}
