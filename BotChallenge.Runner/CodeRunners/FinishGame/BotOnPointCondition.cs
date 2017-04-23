using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners.FinishGame
{
    public class BotOnPointCondition : FinishGameCondition
    {
        private int x, y, maxCommandCount;

        public BotOnPointCondition(int x, int y, int maxCommands = 25)
        {
            this.x = x;
            this.y = y;
            this.maxCommandCount = maxCommands;
        }

        public override bool IsFinished(Field f, int commandsCount)
        {
            if (commandsCount > maxCommandCount)
            {
                return true;
            }

            return f.Points[y][x] == Point.BlueBot || f.Points[y][x] == Point.RedBot;
        }
    }
}
