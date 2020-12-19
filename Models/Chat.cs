using System;
using System.Collections;
using System.Reactive.Subjects;
using System.Reactive.Linq;


namespace Bingo.Models
{
    public class Chat : IChat
    {
        private readonly ISubject<Game> _stream=new ReplaySubject<Game>(1);
       public Game AddGame(Game game){
           _stream.OnNext(game);
           return game;
       }
       public IObservable<Game> Games(){
           return _stream.AsObservable();
       }
    }

}