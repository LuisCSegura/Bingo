using GraphQL.Types;
using Bingo.Repositories;
using Bingo.GraphQL.Types;
using Bingo.Models;

namespace  Bingo.GraphQL {
    class BingoQuery : ObjectGraphType
    {
        public BingoQuery(UserRepository userRepository, GameRepository gameRepository)
        {
            //USERS-----------------------------------------
            Field<ListGraphType<UserType>>("users",
                                             arguments: new QueryArguments(
                                                 new QueryArgument<StringGraphType> { Name = "name" },
                                                 new QueryArgument<StringGraphType> { Name = "email" }
                                             ),
                                             resolve: context => {
                                                 return userRepository.All(context);
                                             });
            Field<UserType>("user",
                              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                              resolve: context => {
                                  return userRepository.Find(context.GetArgument<long>("id"));
                              });
            Field<AuthResponseType>("login",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "input" }),
                              resolve: context => {
                                  return userRepository.Login(context.GetArgument<User>("input"));
                              });
            
            //GAMES-----------------------------------------
            
            Field<ListGraphType<GameType>>("games",
                                             arguments: new QueryArguments(
                                                 new QueryArgument<StringGraphType> { Name = "name" },
                                                 new QueryArgument<DateTimeGraphType> { Name = "startTime" },
                                                 new QueryArgument<StringGraphType> { Name = "link" },
                                                 new QueryArgument<IntGraphType> { Name = "playersNumber" },
                                                 new QueryArgument<IntGraphType> { Name = "gettedNumber" },
                                                 new QueryArgument<BooleanGraphType> { Name = "finished" }
                                             ),
                                             resolve: context => {
                                                 return gameRepository.All(context);
                                             });
            Field<GameType>("game",
                              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                              resolve: context => {
                                  return gameRepository.Find(context.GetArgument<long>("id"));
                              });
            Field<GameType>("gameByLink",
                              arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "link" }),
                              resolve: context => {
                                  return gameRepository.GameByLink(context.GetArgument<string>("link"));
                              });
        }
    }
}