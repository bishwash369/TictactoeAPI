using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;
using TictactoeApi.Models.Dto;
using TictactoeApi.Repositories.GameRepositories;

namespace TictactoeApi.Controllers
{
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

        //[Route("GamesList")]
        [HttpGet]
        public IActionResult GetGames()
        {
            var objList = _gameRepo.GetGames();
            var objDto = new List<GameDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<GameDto>(obj));
            }
            return Ok(objDto);
        }

        //[Route("GameDisplay")]
        [HttpGet("{gameId:int}")]
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

        //[Route("StartGame")]
        [HttpPost]
        public IActionResult CreateGame([FromBody] GameDto gameDto)
        {
            if (gameDto == null)                    
            {
                return BadRequest(ModelState); 
            }

            if (_gameRepo.GameExists(gameDto.Id))
            {
                ModelState.AddModelError("", "Game Exists!"); 
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gameObj = _mapper.Map<Game>(gameDto);

            if (!_gameRepo.StartGame(gameObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {gameObj.Id}");
                return StatusCode(500, ModelState);            
            }

            return CreatedAtRoute("GetGame", new { gameId = gameObj.Id }, gameObj);                  
        }

        //[Route("MakeChanges")]
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
                return StatusCode(500, ModelState);             
            }

            return NoContent();

        }

        /*[HttpDelete("{gameId:int}", Name = "DeleteGame")]
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
                return StatusCode(500, ModelState);             
            }

            return NoContent();
        }
         */
    }
}
