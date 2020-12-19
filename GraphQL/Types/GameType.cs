using GraphQL.Types;
using Bingo.Models;
using Bingo.Repositories;

namespace  Bingo.GraphQL.Types 
{
    class GameType : ObjectGraphType<Game>
    {
        public GameType(GameRepository gameRepository)
        {
            Name = "Game";
            Field(x => x.Id);
            Field(x => x.Name).Description("Game's name");
            Field(x => x.StartTime).Description("Game's start date time");
            Field(x => x.Link).Description("Game's link");
            Field(x => x.PlayersNumber).Description("Game's players number");
            Field(x => x.GettedNumbers).Description("Game's getted numbers");
            Field(x => x.Finished).Description("Game's status");
        }
    }
}