using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;
using AutoMapper;
using TictactoeApi.Models.Dto;

namespace TictactoeApi.Data
{
    public class TicTacToeMappings: Profile
    {
        public TicTacToeMappings()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Player, GameDto>().ReverseMap();
            CreateMap<Player, MoveDto>().ReverseMap();

        }
        
    }
}
