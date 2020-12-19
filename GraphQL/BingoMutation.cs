using GraphQL.Types;
using Bingo.Repositories;
using Bingo.GraphQL.Types;
using Bingo.Models;

namespace  Bingo.GraphQL {
    class BingoMutation : ObjectGraphType
    {
        public BingoMutation(UserRepository userRepository, GameRepository gameRepository, IChat chat)
        {
            //USERS-----------------------------------------           
            Field<UserType>("createUser",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "input" }),
                              resolve: context => {
                                  return userRepository.Add(context.GetArgument<User>("input"));
                              });
            Field<UserType>("updateUser",
                              arguments: new QueryArguments(
                                  new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                                  new QueryArgument<NonNullGraphType<UserInputType>> { Name = "input" }
                              ),
                              resolve: context => {
                                  var id = context.GetArgument<long>("id");
                                  var user = context.GetArgument<User>("input");
                                  return userRepository.Update(id, user);
                              });
            Field<UserType>("deleteUser",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                              resolve: context => {
                                  return userRepository.Remove(context.GetArgument<long>("id"));
                              });
            //GAMES-----------------------------------------                      
            Field<GameType>("createGame",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GameInputType>> { Name = "input" }),
                              resolve: context => {
                                  return gameRepository.Add(context.GetArgument<Game>("input"));
                              });
            Field<GameType>("updateGame",
                              arguments: new QueryArguments(
                                  new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                                  new QueryArgument<NonNullGraphType<GameInputType>> { Name = "input" }
                              ),
                              resolve: context => {
                                  var id = context.GetArgument<long>("id");
                                  var game = context.GetArgument<Game>("input");
                                  var updated=gameRepository.Update(id, game).Result;
                                  return chat.AddGame(updated);
                              });
            Field<GameType>("updateGameSync",
                              arguments: new QueryArguments(
                                  new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                                  new QueryArgument<NonNullGraphType<GameInputType>> { Name = "input" }
                              ),
                              resolve: context => {
                                  var id = context.GetArgument<long>("id");
                                  var game = context.GetArgument<Game>("input");
                                  game.Id=id;
                                  return gameRepository.UpdateSync(id, game);
                              });
            Field<GameType>("deleteGame",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                              resolve: context => {
                                  return gameRepository.Remove(context.GetArgument<long>("id"));
                              });
            Field<GameType>("joinAGame",
                              arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "link" }),
                              resolve: context => {
                                  var link = context.GetArgument<string>("link");
                                  Game game = gameRepository.GameByLink(link);
                                  if(game!=null){
                                    game.PlayersNumber=game.PlayersNumber+1;
                                    var updated=gameRepository.UpdateSync(game.Id, game);
                                    return chat.AddGame(updated);
                                  }
                                  return null;
                                  
                              });
            Field<GameType>("quitTheGame",
                              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                              resolve: context => {
                                  var id = context.GetArgument<long>("id");
                                  Game game = gameRepository.Find(id);
                                  if(game!=null){
                                    if(game.PlayersNumber>0){
                                        game.PlayersNumber=game.PlayersNumber-1;
                                    }
                                    var updated=gameRepository.UpdateSync(game.Id, game);
                                    return chat.AddGame(updated);
                                  }
                                  return null;
                                  
                              });
            //MESSAGES-----------------------------------------  
            Field<GameType>("addGame",
                            arguments: new QueryArguments(new QueryArgument<GameInputType> { Name = "game" }),
                            resolve: context => {
                                return chat.AddGame(context.GetArgument<Game>("game"));
                            });
        }
    }
}