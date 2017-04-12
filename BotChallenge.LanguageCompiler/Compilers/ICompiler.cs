using BotChallenge.Compiler.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler
{
    public interface ICompiler
    {
        bool VerifyCode(TaskParameters task, out List<string> errors, params string[] codeClasses);
    }
}
