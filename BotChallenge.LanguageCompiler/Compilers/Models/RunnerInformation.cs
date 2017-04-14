using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Compilers.Models
{
    public abstract class RunnerInformation
    { 
        public RunnerInformation() { }

        public RunnerInformation(string playerName)
        {
            this.PlayerName = playerName;
        }

        public string PlayerName { get; set; }
    }
}
