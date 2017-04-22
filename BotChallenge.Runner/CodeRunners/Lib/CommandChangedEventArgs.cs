using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners.Lib
{
    class CommandChangedEventArgs : EventArgs
    {
        public CommandChangedEventArgs(GameCommand oldCommand, GameCommand newCommand)
        {
            OldCommand = oldCommand;
            NewCommand = newCommand;
        }

        public GameCommand OldCommand { get; set; }
        public GameCommand NewCommand { get; set; }
    }
}
