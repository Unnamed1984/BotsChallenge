using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler;
using BotChallenge.Runner.CodeRunners;
using BotChallenge.Compiler.LanguageProviders;
using BotChallenge.Runner.LanguageProviders;
using BotChallenge.Runner.CodeRunners.Models;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Compiler.Compilers;
using BotChallenge.BLL.JsonLoad.MapParser;
using BotChallenge.Runner.CodeRunners.FinishGame;

namespace BotChallenge.BLL.Logic
{
    public class BotsRunner
    {
        private ICompiler compiler = null;
        private IRunner runner = null;

        public BotsRunner()
        {
            ILanguageProvider compProvider = new LanguageProvider();
            this.compiler = compProvider.GetCompilerForLanguage(CompilerSupportedLanguages.CSharp);

            IRunnerProvider runProvider = new RunnerProvider();
            this.runner = runProvider.GetRunnerForLanguage(RunnerSupportedLanguages.CSharp);
        }

        public void RunCode(string[] firstPlayerCode, string[] secondPlayerCode, string pathToField, GameFinishType finishType, IEnumerable<Bot> bots1, IEnumerable<Bot> bots2, int finalX = 0, int finalY = 0)
        {
            CompilationResult compResult1 = compiler.CompileCode(TaskParameters.Build(firstPlayerCode.Length), firstPlayerCode);
            CompilationResult compResult2 = compiler.CompileCode(TaskParameters.Build(secondPlayerCode.Length), secondPlayerCode);

            if (!compResult1.IsCodeCorrect || !compResult2.IsCodeCorrect)
            {
                throw new ArgumentException("Unable to compile code");
            }

            FieldBuilder fieldBuilder = new FieldBuilder(pathToField);

            Field field = fieldBuilder.GetFieldForRunner();
            field = fieldBuilder.PlaceBots(field, bots1.Select(b => new Models.Bot(b.X, b.Y, b.Name)), 1);
            field = fieldBuilder.PlaceBots(field, bots2.Select(b => new Models.Bot(b.X, b.Y, b.Name)), 2);

            FinishGameCondition finishCondition = null;

            if (finishType == GameFinishType.CommandsNumber)
            {
                finishCondition = new CommandNumberCondition();
            }
            else
            {
                finishCondition = new BotOnPointCondition(finalX, finalY);
            }

            runner.RunCodeGame(compResult1.InformationForCodeRunner, compResult2.InformationForCodeRunner, field, bots1, bots2, finishCondition);

            runner.GameFinished += Runner_GameFinished;
        }

        private void Runner_GameFinished(object sender, GameFinishedEventArgs e)
        {
            GameFinished?.Invoke(this, e);
        }

        public event EventHandler<GameFinishedEventArgs> GameFinished;
    }
}
