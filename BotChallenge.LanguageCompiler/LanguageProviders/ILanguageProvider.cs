using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.CodeRunners;

namespace BotChallenge.Compiler.LanguageProviders
{

    public interface ILanguageProvider
    {
        ICompiler GetCompilerForLanguage(CompilerSupportedLanguages language);
        IRunner GetRunnerForLanguage(CompilerSupportedLanguages language);
    }
}
