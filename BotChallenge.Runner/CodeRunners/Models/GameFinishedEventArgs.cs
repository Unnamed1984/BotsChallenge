using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Runner.CodeRunners.Models
{
    class GameFinishedEventArgs : EventArgs
    {
        public GameFinishedEventArgs(string winnerName, IEnumerable<GameCommand> commands)
        {
            WinnerName = winnerName;
            Commands = commands;
        }

        public string WinnerName { get; }
        public IEnumerable<GameCommand> Commands { get; }
    }
}
