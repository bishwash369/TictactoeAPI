using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Data;
using TictactoeApi.Models;
using TictactoeApi.Models.Dto;

namespace TictactoeApi.Controllers
{
    [Route("api/Moves")]
    [ApiController]
    public class MovesController : ControllerBase
    {
         public readonly ApplicationDbContext db;
         //public int count;

        public MovesController(ApplicationDbContext DbContext)
        {
            db = DbContext;
        }
        [HttpPost]
        [Route("Move")]
        public async Task<bool> Move([FromBody] MoveDto move)
        {
           
            var myMove = new Move()
            {
                MoveType = move.MoveType,
                Position = move.Position,
                PlayerId = move.PlayerId,
                GameId = move.GameId,
                //count++;
                //return count;
            };

            var count = db.Moves.Where(x => x.GameId == move.GameId).Count();
            if (count >= 5)
            {
                var data = db.Moves.Where(x => x.GameId == move.GameId).Select(x => x.Position).ToArray();
            }

            var data = db.Moves.Where(x => x.GameId == move.GameId && x.MoveType == move.MoveType).Select(x => x.Position).ToArray();



            await db.Moves.AddAsync(myMove);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
