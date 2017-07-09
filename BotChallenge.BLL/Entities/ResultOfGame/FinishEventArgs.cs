using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Entities.ResultOfGame
{
    public class FinishEventArgs
    {
        public string WinnerName { get; set; }
        public ICollection<Command> Commands { get; set; }
    }
}
