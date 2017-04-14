using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Compiler.CodeRunners.Models;

namespace BotChallenge.Compiler.CodeRunners
{
    public interface IRunner
    {
        string RunCodeGame(RunnerInformation player1Info, RunnerInformation player2Info, Field field);
    }
}
