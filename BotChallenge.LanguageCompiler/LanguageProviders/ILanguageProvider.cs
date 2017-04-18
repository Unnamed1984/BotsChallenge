
namespace BotChallenge.Compiler.LanguageProviders
{

    public interface ILanguageProvider
    {
        ICompiler GetCompilerForLanguage(CompilerSupportedLanguages language);
    }
}
