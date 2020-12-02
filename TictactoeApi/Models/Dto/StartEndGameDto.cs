using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models.Dto
{
    public class StartEndGameDto
    {
        public bool Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FirstPlayerId { get; set; }
        public int LastPlayerId { get; set; }
    }
}
