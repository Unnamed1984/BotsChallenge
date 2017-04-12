using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Compilers
{
    public class TaskParameters
    {
        private TaskParameters() { }

        internal int RequiredBots { get; set; }

        public static TaskParameters Build(int botsOnTheField) => new TaskParameters() { RequiredBots = botsOnTheField };
        public static TaskParameters Build() => new TaskParameters();

        public TaskParameters AddBots(int number)
        {
            RequiredBots += number;
            return this;
        }
    }
}
