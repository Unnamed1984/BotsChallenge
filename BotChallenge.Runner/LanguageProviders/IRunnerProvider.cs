using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Runner.CodeRunners;

namespace BotChallenge.Runner.LanguageProviders
{

    public interface IRunnerProvider
    {
        IRunner GetRunnerForLanguage(RunnerSupportedLanguages language);
    }
}
