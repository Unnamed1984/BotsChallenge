using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Runner.CodeRunners;
using BotChallenge.Runner.Exceptions;

namespace BotChallenge.Runner.LanguageProviders
{
    public class RunnerProvider : IRunnerProvider
    {
        public IRunner GetRunnerForLanguage(RunnerSupportedLanguages language)
        {
            if (language == RunnerSupportedLanguages.CSharp)
            {
                return new CSharpRunner();
            }

            throw new UnSupportedLanguageException("There is no support for this language in system.");
        }
    }
}
