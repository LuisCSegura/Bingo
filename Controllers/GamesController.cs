using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bingo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Bingo.Hubs;


namespace Bingo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly IHubContext<Messages> _hubContext;
        public GamesController(DataBaseContext context, IHubContext<Messages> hubContext)
        {
            _context = context;
             _hubContext=hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(long id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return game;
        }
        [HttpPost("gamebylink")]
        public IActionResult GameByLink(Game request)
        {
            IActionResult result= NotFound();
            var game = this._context.Games.SingleOrDefault(g=> g.Link == request.Link);
            if(game!=null){
                result=Ok(game);
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Game>> PostUser(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(long id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }


        [HttpPut("{id}")]
        public IActionResult PutGame(long id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool GameExists(long id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
        [HttpPost("sendgame")]
        public IActionResult SendGame (Game game)
        {
            string gam = Newtonsoft.Json.JsonConvert.SerializeObject(game);
            //send to hub
            _hubContext.Clients.All.SendAsync("notifyall", gam);
            return Ok(new{response="Todo bien todo correcto y yo que me alegro", game= gam});
        }
    }
}