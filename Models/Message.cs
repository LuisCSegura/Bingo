using System;
using System.Collections;
namespace Bingo.Models
{
    public class Message
    {
        public long GameId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int PlayersNumber { get; set; }
        public int[] GettedNumbers { get; set; }
        public bool Finished { get; set; }
    }

}