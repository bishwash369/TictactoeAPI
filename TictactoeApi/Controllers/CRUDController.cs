using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Controllers.Dto;
using TictactoeApi.Data;
using TictactoeApi.Models;

namespace TictactoeApi.Controllers
{
    [ApiController]
    public class CRUDController:ControllerBase
    {
        public readonly ApplicationDbContext dbContext;
        public CRUDController(ApplicationDbContext DbContext)
        {
            dbContext = DbContext;
        }
        [HttpGet]
        [Route("api/GetAllData")]
        public async Task<List<GetDataDto>> GetAllData()
        {
            return await dbContext.Players.Select(x => new GetDataDto
            {
                Name = x.Name,
                Address = x.Address,
                Email = x.Email,
                Games = x.Games.Select(x=> new GamerDto { }).ToList()
            }).ToListAsync();
        }
        [Route("api/CreateData")]
        [HttpPost]
        public async Task<bool> CreateData([FromBody] CreatePlayersDto createPlayer)
        {
            var create = new Player()
            {
                Name = createPlayer.Name,
                Address = createPlayer.Address,
                Email = createPlayer.Email
            };
            await dbContext.Players.AddAsync(create);
            await dbContext.SaveChangesAsync();
            return true;
        }
        [Route("api/UpdateData")]
        [HttpPut]
        public async Task<bool> UpdateData([FromBody]UpdatePlayerDto update)
        {
            var data = await dbContext.Players.Where(x => x.Id == update.Id).FirstOrDefaultAsync();
            data.Name = update.Name;                    // +" "+"Bishwas";
            data.Address = update.Address;
            data.Email = update.Email;
            dbContext.Players.Update(data);
            await dbContext.SaveChangesAsync();
            return true;
        }
        [Route("api/DeleteData/{Id}")]
        [HttpDelete]
        public async Task<bool> DeleteData(int Id)
        {
            var data= await dbContext.Players.Where(x => x.Id == Id).FirstOrDefaultAsync();
            dbContext.Players.Remove(data);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
