using GraphQL.Types;
using Bingo.Models;

namespace  Bingo.GraphQL.Types 
{
    class SendedGameInputType : InputObjectGraphType
    {
        public SendedGameInputType()
        {
            Name = "SendedGameInput";
            Field<NonNullGraphType<IntGraphType>>("id");
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<DateTimeGraphType>>("startTime");
            Field<NonNullGraphType<StringGraphType>>("link");
            Field<NonNullGraphType<IntGraphType>>("playersNumber");
            Field<NonNullGraphType<ListGraphType<IntGraphType>>>("gettedNumbers");
            Field<NonNullGraphType<BooleanGraphType>>("finished");
        }
    }
}