using GraphQL.Types;
using Bingo.Models;
using Bingo.Repositories;

namespace  Bingo.GraphQL.Types 
{
    class MessageType : ObjectGraphType<Message>
    {
        public MessageType()
        {
            Name = "Message";
            Field(x => x.GameId);
            Field(x => x.Name).Description("Game's name");
            Field(x => x.StartTime).Description("Game's start date time");
            Field(x => x.PlayersNumber).Description("Game's players number");
            Field(x => x.GettedNumbers).Description("Game's getted numbers");
            Field(x => x.Finished).Description("Game's status");

        }
    }
}