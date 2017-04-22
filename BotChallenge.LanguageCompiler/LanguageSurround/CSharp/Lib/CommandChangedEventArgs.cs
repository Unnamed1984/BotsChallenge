using System;
using Bots.Models;

namespace Bots.Lib
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
