using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners.Lib
{
    public class BotJournalEventArgs : EventArgs
    {
        public BotJournalEventArgs(string lastLine, Field field)
        {
            LastLine = lastLine;
            Field = field;
        }

        public string LastLine { get; set; }
        public Field Field { get; set; }
    }
}
