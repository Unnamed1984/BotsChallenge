using BotChallenge.Runner.CodeRunners;

namespace BotChallenge.Runner.LanguageProviders
{

    public interface IRunnerProvider
    {
        IRunner GetRunnerForLanguage(RunnerSupportedLanguages language);
    }
}
