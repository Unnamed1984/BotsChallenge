using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.DAL.Shared.Models
{
    public class Game
    {
        public string GameId { get; set; }
        public string MapId { get; set; }
        public string MapName { get; set; }
        public int BotNumber { get; set; }
        public string Name { get; set; }
    }
}
