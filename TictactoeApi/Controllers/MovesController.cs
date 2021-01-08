using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [Route("AllMoves")]
        public async Task<List<MoveDto>> AllMoves()
        {
            return await db.Moves.Select(x => new MoveDto
            {
                MoveType = x.MoveType,
                Position = x.Position,
                PlayerId = x.PlayerId,
                GameId = x.GameId,
            }).ToListAsync();
        }


        [HttpPost]
        [Route("Move")]
        public async Task<IActionResult> Move([FromBody] MoveDto move)
        {
            var count = db.Moves.Where(x => x.GameId == move.GameId).Count();
            if (count >= 5)
            {
                var data = db.Moves.Where(x => x.GameId == move.GameId).Select(x => x.Position).ToList();
                var value = db.Moves.Where(x => x.GameId == move.GameId && x.MoveType == move.MoveType).Select(x => x.Position).ToList();
                var arr = db.Moves.Where(x => x.MoveType == move.MoveType).OrderBy(x => x.Position).ToList();

                List<List<int>> test = new List<List<int>>
                    {
                       new List<int>() { 1, 2, 3 },
                       new List<int>() { 4, 5, 6 },
                       new List<int>() { 7, 8, 9 },
                       new List<int>() { 1, 4, 7 },
                       new List<int>() { 2, 5, 8 },
                       new List<int>() { 3, 6, 9 },
                       new List<int>() { 1, 5, 9 },
                       new List<int>() { 3, 5, 7 },
                    };

                var allNumbers = from l in test
                                 let numbers = l
                                 from n in numbers
                                 select n;
                foreach (var n in allNumbers)
                {
                    foreach (var a in arr)
                    {
                        var v = n.Equals(a);
                        if (move.MoveType == "x")
                        {
                            if (v)
                            {
                                return Ok("Winner is Player" + move.PlayerId);
                            }
                        }
                        else if(move.MoveType == "x")
                        {
                            if (v)
                            {
                                return Ok("Winner is Player" + move.PlayerId);
                            }
                        }
                        else
                        {
                            return Ok("Game ends in a Tie");
                        }
                    }
                }
            
            }
            var myMove = new Move()
            {
                MoveType = move.MoveType,
                Position = move.Position,
                PlayerId = move.PlayerId,
                GameId = move.GameId,
            };
            await db.Moves.AddAsync(myMove);
            await db.SaveChangesAsync();
            return Ok(true);

        }
    }
      

}
