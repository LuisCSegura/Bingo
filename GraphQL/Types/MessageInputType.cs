using GraphQL.Types;
using Bingo.Models;

namespace  Bingo.GraphQL.Types 
{
    class MessageInputType : InputObjectGraphType
    {
        public MessageInputType()
        {
            Name = "MessageInput";
            Field<NonNullGraphType<IntGraphType>>("gameId");
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<DateTimeGraphType>>("startTime");
            Field<NonNullGraphType<IntGraphType>>("playersNumber");
            Field<NonNullGraphType<ListGraphType<IntGraphType>>>("gettedNumbers");
            Field<NonNullGraphType<BooleanGraphType>>("finished");
        }
    }
}