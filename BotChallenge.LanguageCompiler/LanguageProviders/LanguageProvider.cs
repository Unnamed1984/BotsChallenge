using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers;
using BotChallenge.Compiler.Exceptions;

namespace BotChallenge.Compiler.LanguageProviders
{
    public class LanguageProvider : ILanguageProvider
    {
        public ICompiler GetCompilerForLanguage(CompilerSupportedLanguages language)
        {
            if (language == CompilerSupportedLanguages.CSharp)
            {
                return new CSharpCompiler();
            }

            throw new UnSupportedLanguageException("There is no support for this language in system.");
        }
    }
}
