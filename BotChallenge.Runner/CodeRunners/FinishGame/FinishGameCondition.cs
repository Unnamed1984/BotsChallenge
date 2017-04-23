using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners.FinishGame
{
    public abstract class FinishGameCondition
    {
        public abstract bool IsFinished(Field f, int commandsCount);
    }
}
