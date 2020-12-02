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

            await db.Moves.AddAsync(myMove);
            await db.SaveChangesAsync();
            return true;


            var count = db.Moves.Where(x => x.GameId == move.GameId).Count();
            if (count >= 5)
            {
                var data = db.Moves.Where(x => x.GameId == move.GameId).Select(x => x.Position).ToArray();
                var value = db.Moves.Where(x => x.GameId == move.GameId && x.MoveType == move.MoveType).Select(x => x.Position).ToArray();
                var arr = db.Moves.Where(x => x.MoveType == move.MoveType).OrderBy(x => x.Position).ToList();
                int[] arr1 =
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 9 },
                        { 1, 4, 7 },
                        { 2, 5, 8 },
                        { 3, 6, 9 },
                        { 1, 5, 9 },
                        { 3, 5, 7 },
                    };
                
                if (move.MoveType == "x")
                {
                    for(int i=0; i<arr1.Length;i++)
                    { 
                        if(arr == arr1[i])
                        { 
                            throw new ArgumentException("Winner is Player" + move.PlayerId);
                        }
                    }
                } 
                else if (move.MoveType == "o")
                {
                    for (int i = 0; i < arr1.Length; i++)
                    {
                        if (arr == arr1[i])
                        {
                            throw new ArgumentException("Winner is Player" + move.PlayerId);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Game ends in a Tie");
                }
                
            }
            else
            {
                throw new ArgumentException("Invalid");
            }

        }
    }
}
