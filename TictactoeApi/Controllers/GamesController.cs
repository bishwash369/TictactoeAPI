using AutoMapper;
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
    [Route("api/Games")]
    [ApiController]
    public class GameContoller : ControllerBase
    {
        public readonly ApplicationDbContext db;

        public GameContoller(ApplicationDbContext DbContext)
        {
            db = DbContext;

        }

        [HttpGet]
        [Route("GetAllGames")]
        public async Task<List<GameDto>> GetAllGames()
        {
            return await db.Games.Select(x => new GameDto
            {
                Id = x.Id,
                Status = x.Status,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                FirstPlayerId = x.FirstPlayerId,
                LastPlayerId = x.LastPlayerId
            }).ToListAsync();
        }

        [HttpPost]
        [Route("StartGame")]
        public async Task<bool> StartGame([FromBody] StartEndGameDto game)
        {
            var create = new Game()
            {
                Status = true,
                StartTime = DateTime.Now,
                //EndTime = game.EndTime,
                FirstPlayerId = game.FirstPlayerId,
                LastPlayerId = game.LastPlayerId
            };
            await db.Games.AddAsync(create);
            await db.SaveChangesAsync();
            return true;
        }

        [HttpPost]
        [Route("EndGame")]
        public async Task<bool> EndGame([FromBody] StartEndGameDto game)
        {
            var create = new Game()
            {
                Status = false,
                EndTime = DateTime.Now,
                //FirstPlayerId = game.FirstPlayerId,
                //LastPlayerId = game.LastPlayerId
            };
            await db.Games.AddAsync(create);
            await db.SaveChangesAsync();
            return true;
        }
    }
}



        /* [HttpPut]
         [Route("UpdateGame/{Id}")]
         public async Task<bool> UpdateGame([FromBody] GameDto game)
         {
             var query = await db.Games.Where(x => x.Id == game.Id).FirstOrDefaultAsync();
             query.Status = game.Status;
             query.StartTime = game.StartTime;
             query.EndTime = game.EndTime;
             query.FirstPlayerId = game.FirstPlayerId;
             query.LastPlayerId = game.LastPlayerId;
             db.Games.Update(query);
             await db.SaveChangesAsync();
             return true;
         }

         [HttpDelete]
         [Route("DeleteGame/{Id}")]
         public async Task<bool> DeleteGame(int Id)
         {
             var query = await db.Games.Where(x => x.Id == Id).FirstOrDefaultAsync();
             db.Games.Remove(query);
             await db.SaveChangesAsync();
             return true;
         }
        */
/*
[Route("api/Games")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameRepository _gameRepo;
    private readonly IMapper _mapper;

    public GamesController(IGameRepository gameRepo, IMapper mapper)
    {
        _gameRepo = gameRepo;
        _mapper = mapper;

    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<GameDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetGames()
    {
        //var objList =_gameRepo.GetGames();

       // var objDto = new List<GameDto>();

      //  foreach (var obj in objList)
        //{
          //  objDto.Add(_mapper.Map<GameDto>(obj));     
       // }  
        objDto.AddRange(_mapper.Map<List<GameDto>>(objDto));
        return Ok(objDto);
    }


    [HttpGet("{gameId:int}", Name = "GetGame")]
    public IActionResult GetGame(int gameId)
    {
        var obj = _gameRepo.GetGame(gameId);
        if (obj == null)
        {
            return NotFound();
        }
        var objDto = _mapper.Map<GameDto>(obj);
        return Ok(objDto);

    }

    [HttpPost]
    public IActionResult CreateGame([FromBody] GameDto gameDto)
    {
        if (gameDto == null)                    //if null then 400 badrequest i.e. client error(invalid syntaz/request/message, etc)
        {
            return BadRequest(ModelState);              //Modelstate stores validation errors and returns them if error is encountered
        }

        if (_gameRepo.GameExists(gameDto.Id))
        {
            ModelState.AddModelError("", "Game Exists!");          //returns if an existing item is posted again
            return StatusCode(404, ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var gameObj = _mapper.Map<Game>(gameDto);

        if (!_gameRepo.CreateGame(gameObj))
        {
            ModelState.AddModelError("", $"Something went wrong when saving the record {gameObj.Id}");
            return StatusCode(500, ModelState);            
        }

        return CreatedAtRoute("GetGame", new { gameId = gameObj.Id }, gameObj);                 
    }

    [HttpPatch("{gameId:int}", Name = "UpdateGame")]

    public IActionResult UpdateGame(int gameId, [FromBody] GameDto gameDto)
    {
        if (gameDto == null || gameId != gameDto.Id)
        {
            return BadRequest(ModelState);
        }

        var gameObj = _mapper.Map<Game>(gameDto);
        if (!_gameRepo.UpdateGame(gameObj))
        {
            ModelState.AddModelError("", $"Something went wrong when updating the record {gameObj.Id}");
            return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
        }

        return NoContent();

    }

    [HttpDelete("{gameId:int}", Name = "DeleteGame")]
    public IActionResult DeleteGame(int gameId)
    {
        if (!_gameRepo.GameExists(gameId))
        {
            return NotFound();
        }

        var gameObj = _gameRepo.GetGame(gameId);
        if (!_gameRepo.DeleteGame(gameObj))
        {
            ModelState.AddModelError("", $"Something went wrong when deleting the record {gameObj.Id}");
            return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
        }

        return NoContent();

    }
*/



