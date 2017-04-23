using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners.FinishGame
{
    public class CommandNumberCondition : FinishGameCondition
    {
        private int commandsMax;

        public CommandNumberCondition(int commandsMax = 10)
        {
            this.commandsMax = commandsMax;
        }

        public override bool IsFinished(Field f, int commandsCount)
        {
            return commandsCount > commandsMax;
        }
    }
}
