using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners
{
    public interface IRunner
    {
        string RunCodeGame(RunnerInformation player1Info, RunnerInformation player2Info, Field field);
    }
}
