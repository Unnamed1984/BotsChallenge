using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Compilers.Models
{
    public class CSharpRunnerInformation : RunnerInformation
    {
        public CSharpRunnerInformation(string path)
        {
            PathToExecutable = path;
        }

        public CSharpRunnerInformation(string path, string playerName) : base(playerName)
        {
            PathToExecutable = path;
        }

        public string PathToExecutable { get; set; }
    }
}
