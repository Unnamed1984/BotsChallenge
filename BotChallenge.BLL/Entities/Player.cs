using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Models
{
    public class Player
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Game Game { get; set; }
        public HashSet<string> ConnectionIds { get; set; }


        public bool SetReady()
        {
            return true;
        }
    }
}
