using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Models.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
