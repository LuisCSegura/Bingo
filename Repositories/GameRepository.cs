using Bingo.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Types;
using Bingo.GraphQL;
using Microsoft.EntityFrameworkCore;

namespace Bingo.Repositories
{
    class GameRepository
    {
        private readonly DataBaseContext _context;
        public GameRepository(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> All(ResolveFieldContext<object> context){
            var results = from games in _context.Games select games;
            if (context.HasArgument("name"))
            {
                var value = context.GetArgument<string>("name");
                results = results.Where(a => a.Name.Contains(value));
            }
            if (context.HasArgument("startTime"))
            {
                var value = context.GetArgument<DateTimeGraphType>("startTime");
                results = results.Where(a => a.StartTime.Equals(value));
            }
            if (context.HasArgument("link"))
            {
                var value = context.GetArgument<string>("link");
                results = results.Where(a => a.Link.Contains(value));
            }
            if (context.HasArgument("playersNumber"))
            {
                var value = context.GetArgument<int>("playersNumber");
                results = results.Where(a => a.PlayersNumber==value);
            }
            if (context.HasArgument("gettedNumber"))
            {
                var value = context.GetArgument<int>("gettedNumber");
                results = results.Where(a => a.GettedNumbers.Contains(value));
            }
            if (context.HasArgument("finished"))
            {
                var value = context.GetArgument<bool>("finished");
                results = results.Where(a => a.Finished==value);
            }
            results=results.OrderBy(a => a.Id);
            return results;
        }

        public Game Find(long id){
            return _context.Games.Find(id);
        }

        public async Task<Game> Add(Game game) {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game> Update(long id, Game game) {
            game.Id = id;
            var updated = (_context.Games.Update(game)).Entity;
            if (updated == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return updated;
        }


        public async Task<Game> Remove(long id) {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return null;
            }
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return game;
        }
        public Game GameByLink(string request)
        {
            Game result= null;
            var game = this._context.Games.SingleOrDefault(g=> g.Link == request);
            if(game!=null){
                result= game;
            }
            return result;
        }
        public Game UpdateSync(long id, Game game) {
            if (id != game.Id)
            {
                return null;
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return game;
        }
    }
}