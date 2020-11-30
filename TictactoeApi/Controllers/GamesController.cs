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

        [HttpPost]
        public IActionResult CreateGame([FromBody] GameDto gameDto)
        {
            if (gameDto == null)                    //if null then 400 badrequest i.e. client error(invalid syntaz/request/message, etc)
            {
                return BadRequest(ModelState);              //Modelstate stores validation errors and returns them if error is encountered
            }

            if (_gameRepo.GameExists(gameDto.Name))
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
                ModelState.AddModelError("", $"Something went wrong when saving the record {gameObj.Name}");
                return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
            }

            return CreatedAtRoute("GetGame", new { gameId = gameObj.Id }, gameObj);                   //creates and returns by Id after routing
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
                ModelState.AddModelError("", $"Something went wrong when updating the record {gameObj.Name}");
                return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
            }

            return NoContent();

        }

        [HttpDelete("{gameId:int}", Name = "UpdateGame")]
        public IActionResult DeleteGame(int gameId)
        {
            if (!_gameRepo.GameExists(gameId))
            {
                return NotFound();
            }

            var gameObj = _gameRepo.GetGame(gameId);
            if (!_gameRepo.DeleteGame(gameObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {gameObj.Name}");
                return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
            }

            return NoContent();

        }
    }
}
