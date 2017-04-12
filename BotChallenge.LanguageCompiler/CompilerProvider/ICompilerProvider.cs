using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.CompilerProvider
{

    public interface ICompilerProvider
    {
        ICompiler GetCompilerForLanguage(CompilerSupportedLanguages language);
    }
}
