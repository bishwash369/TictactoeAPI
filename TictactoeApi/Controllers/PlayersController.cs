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

       // [Route("GetPlayers")]
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

        //[Route("GetPlayer")]
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
        
        //[Route("CreatePlayer")]
        [HttpPost]
        public IActionResult CreatePlayer([FromBody] PlayerDto playerDto)
        {
            if (playerDto == null)                    
            {
                return BadRequest(ModelState);              
            }

            if (_playerRepo.PlayerExists(playerDto.Name))
            {
                ModelState.AddModelError("", "Player Exists!");          
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
                return StatusCode(500, ModelState);             
            }

            return CreatedAtRoute("GetPlayer", new { playerId = playerObj.Id }, playerObj);                   
        }

        //[Route("UpdatePlayer")]
        [HttpPatch("{playerId:int}", Name = "UpdatePlayer")]
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
                return StatusCode(500, ModelState);             
            }

            return NoContent();

        }
                
        //[Route("DeletePlayer")]
        [HttpDelete("{playerId:int}", Name = "DeletePlayer")]
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
                return StatusCode(500, ModelState);             
            }

            return NoContent();

        }


    }
}
