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
using BotChallenge.BLL.Models;
using AutoMapper;

namespace BotChallenge.BLL.Logic
{
    public class BotsRunner
    {
        private ICompiler compiler = null;
        private IRunner runner = null;
        private IMapper mapper = null;

        public BotsRunner()
        {
            ILanguageProvider compProvider = new LanguageProvider();
            this.compiler = compProvider.GetCompilerForLanguage(CompilerSupportedLanguages.CSharp);

            IRunnerProvider runProvider = new RunnerProvider();
            this.runner = runProvider.GetRunnerForLanguage(RunnerSupportedLanguages.CSharp);

            MapperConfiguration mapperConfig = new MapperConfiguration(conf =>
            {
                conf.CreateMap<BLL.Models.Bot, Runner.CodeRunners.Models.Bot>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        public void RunCode(string[] firstPlayerCode, string[] secondPlayerCode, string pathToField, GameFinishType finishType, IEnumerable<BLL.Models.Bot> bots1, IEnumerable<BLL.Models.Bot> bots2, int finalX = 0, int finalY = 0)
        {
            CompilationResult compResult1 = compiler.CompileCode(TaskParameters.Build(firstPlayerCode.Length), firstPlayerCode);
            CompilationResult compResult2 = compiler.CompileCode(TaskParameters.Build(secondPlayerCode.Length), secondPlayerCode);

            if (!compResult1.IsCodeCorrect || !compResult2.IsCodeCorrect)
            {
                throw new ArgumentException("Unable to compile code");
            }

            FieldBuilder fieldBuilder = new FieldBuilder(pathToField);

            Runner.CodeRunners.Models.Field field = fieldBuilder.GetFieldForRunner();
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

            IEnumerable<Runner.CodeRunners.Models.Bot> runnerBots1 = bots1.Select(mapper.Map<Runner.CodeRunners.Models.Bot>);
            IEnumerable<Runner.CodeRunners.Models.Bot> runnerBots2 = bots2.Select(mapper.Map<Runner.CodeRunners.Models.Bot>);

            runner.RunCodeGame(compResult1.InformationForCodeRunner, compResult2.InformationForCodeRunner, field, runnerBots1, runnerBots2, finishCondition);

            runner.GameFinished += Runner_GameFinished;
        }

        private void Runner_GameFinished(object sender, GameFinishedEventArgs e)
        {
            GameFinished?.Invoke(this, e);
        }

        public event EventHandler<GameFinishedEventArgs> GameFinished;
    }
}
