using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;
using TictactoeApi.Models.Dto;
using TictactoeApi.Repositories;

namespace TictactoeApi.Controllers
{   
    [Route("api/Players")]
    [ApiController]
    public class PlayersController: ControllerBase
    {
        private readonly ITictactoeRepository _playerRepo;
        private readonly IMapper _mapper;

        public PlayersController(ITictactoeRepository playerRepo, IMapper mapper)
        {
            _playerRepo = playerRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            var objList = _playerRepo.GetPlayers();
            var objDto = new List<PlayerDto>();

            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<PlayerDto>(obj));
            }
            return Ok(objDto);
        }

        [HttpGet("{playerId:int}")]
        public IActionResult GetPlayer(int playerId)
        {
            var obj = _playerRepo.GetPlayer(playerId);
            if(obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<PlayerDto>(obj);
            return Ok(objDto);
        }

        public IActionResult CreatePlayer([FromBody] PlayerDto playerDto)
        {
            if (playerDto == null)                    //if null then 400 badrequest i.e. client error(invalid syntaz/request/message, etc)
            {
                return BadRequest(ModelState);              //Modelstate stores validation errors and returns them if error is encountered
            }

            if (_playerRepo.PlayerExists(playerDto.Name))
            {
                ModelState.AddModelError("", "Player Exists!");          //returns if an existing item is posted again
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playerObj = _mapper.Map<Player>(playerDto);

            if (!_playerRepo.CreatePlayer(playerObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {playerObj.Name}");
                return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
            }

            return CreatedAtRoute("GetPlayer", new { playerId = playerObj.Id }, playerObj);                   //creates and returns by Id after routing
        }

        [HttpPatch("{playerId:int}", Name = "UpdatePlayer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePlayer(int playerId, [FromBody] PlayerDto playerDto)
        {
            if (playerDto == null || playerId != playerDto.Id)
            {
                return BadRequest(ModelState);
            }

            var playerObj = _mapper.Map<Player>(playerDto);
            if (!_playerRepo.UpdatePlayer(playerObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {playerObj.Name}");
                return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
            }

            return NoContent();

        }

        [HttpDelete("{playerId:int}", Name = "UpdatePlayer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlayer(int playerId)
        {
            if (!_playerRepo.PlayerExists(playerId))
            {
                return NotFound();
            }

            var playerObj = _playerRepo.GetPlayer(playerId);
            if (!_playerRepo.DeletePlayer(playerObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {playerObj.Name}");
                return StatusCode(500, ModelState);             //statuscode 500 is returns internal server error
            }

            return NoContent();

        }


    }
}
