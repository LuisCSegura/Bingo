using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bingo.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Bingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public IConfiguration _config { get; }

        public AuthController(DataBaseContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User request)
        {
            _context.Users.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = request.Id }, request);
        }

        [HttpPost("login")]
        public IActionResult Login(User request)
        {
            IActionResult response = Unauthorized();
            var usr = this._context.Users.SingleOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (usr == null)
            {
                return Unauthorized("Invalid Email or password");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings").GetSection("APP_KEY").Value);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, usr.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return Ok(new { token = tokenHandler.WriteToken(token), user = usr});
        }


    }
}