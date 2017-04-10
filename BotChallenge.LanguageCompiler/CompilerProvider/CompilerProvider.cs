using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.LanguageCompiler.Compilers;
using BotChallenge.LanguageCompiler.Exceptions;

namespace BotChallenge.LanguageCompiler.CompilerProvider
{
    class CompilerProvider : ICompilerProvider
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
