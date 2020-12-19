using System;
using System.Collections;
namespace Bingo.Models
{
    interface IChat
    {
        Game AddGame(Game game);
        IObservable<Game> Games();
    }

}