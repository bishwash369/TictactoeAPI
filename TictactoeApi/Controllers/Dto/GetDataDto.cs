using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TictactoeApi.Controllers.Dto
{
    public class GetDataDto
    {
        public GetDataDto()
        {
            Games = new List<GamerDto>();
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<GamerDto> Games { get; set; }
    }
}
