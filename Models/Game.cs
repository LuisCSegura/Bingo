using System;
namespace Bingo.Models
{
    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public string Link { get; set; }
        public int PlayersNumber { get; set; }
        public int[] GettedNumbers { get; set; }
        public bool Finished { get; set; }
        

    }

}