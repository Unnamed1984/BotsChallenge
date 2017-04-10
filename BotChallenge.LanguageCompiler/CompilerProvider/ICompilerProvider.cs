using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.LanguageCompiler.CompilerProvider
{

    public interface ICompilerProvider
    {
        ICompiler GetCompilerForLanguage(CompilerSupportedLanguages language);
    }
}
