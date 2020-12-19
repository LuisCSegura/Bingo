using System;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using Bingo.GraphQL.Types;
using Bingo.Models;

namespace  Bingo.GraphQL 
{
    class BingoSuscription:  ObjectGraphType
    {
        private readonly IChat _chat;
        public  BingoSuscription(IChat chat){
            _chat=chat;
            AddField(
                new EventStreamFieldType{
                    Name= "gameReceived",
                    Type= typeof(GameType),
                    Subscriber=new EventStreamResolver<Game>(Subscribe),
                    Resolver=new FuncFieldResolver<Game>(ResolveGame),
                }
            );
        }
        private Game ResolveGame(ResolveFieldContext<object> context){
            var game=context.Source as Game;
            return game;
        }
        private IObservable<Game> Subscribe(ResolveFieldContext<object> context){
            return _chat.Games();
        }


    }
}