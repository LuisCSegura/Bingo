using System;
using Bingo.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Types;
using Bingo.GraphQL;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Bingo.Repositories
{
    class UserRepository
    {
        private readonly DataBaseContext _context;
        public IConfiguration _config { get; }
        public UserRepository(DataBaseContext context,IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public IEnumerable<User> All(ResolveFieldContext<object> context){
            var results = from users in _context.Users select users;
            if (context.HasArgument("name"))
            {
                var value = context.GetArgument<string>("name");
                results = results.Where(a => a.Name.Contains(value));
            }
            if (context.HasArgument("email"))
            {
                var value = context.GetArgument<string>("email");
                results = results.Where(a => a.Email.Contains(value));
            }
            return results;
        }

        public User Find(long id){
            return _context.Users.Find(id);
        }

        public async Task<User> Add(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(long id, User user) {
            user.Id = id;
            var updated = (_context.Users.Update(user)).Entity;
            if (updated == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return updated;
        }

        public async Task<User> Remove(long id) {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public AuthResponse Login(User request){
            AuthResponse response = null;
            var usr = this._context.Users.SingleOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (usr == null)
            {
                return response;
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
            response=new AuthResponse();
            response.Token=tokenHandler.WriteToken(token);
            response.UserName=usr.Name;
            response.Id=usr.Id;
            return response;
        }
    }
}