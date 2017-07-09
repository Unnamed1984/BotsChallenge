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
using BotChallenge.BLL.Entities.ResultOfGame;
using AutoMapper;
using BotChallenge.BLL.Models;

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

        public void RunCode(Game g, string pathToField, GameFinishType finishType, int finalX = 0, int finalY = 0)
        {
            Player firstPlayer = g.Players.First();

            CompilationResult compResult1 = compiler.CompileCode(TaskParameters.Build(firstPlayer.BotsCount), firstPlayer.BotsCode);
            compResult1.InformationForCodeRunner.PlayerName = firstPlayer.Name;

            Player secondPlayer = g.Players.Last();
            CompilationResult compResult2 = compiler.CompileCode(TaskParameters.Build(secondPlayer.BotsCount), secondPlayer.BotsCode);
            compResult2.InformationForCodeRunner.PlayerName = secondPlayer.Name;

            if (!compResult1.IsCodeCorrect || !compResult2.IsCodeCorrect)
            {
                throw new ArgumentException("Unable to compile code");
            }

            FieldBuilder fieldBuilder = new FieldBuilder(pathToField);

            Runner.CodeRunners.Models.Field field = fieldBuilder.GetFieldForRunner();
            field = fieldBuilder.PlaceBots(field, g.Field.Bots[firstPlayer.Name], 1);
            field = fieldBuilder.PlaceBots(field, g.Field.Bots[secondPlayer.Name], 2);

            FinishGameCondition finishCondition = null;

            if (finishType == GameFinishType.CommandsNumber)
            {
                finishCondition = new CommandNumberCondition();
            }
            else
            {
                finishCondition = new BotOnPointCondition(finalX, finalY);
            }

            IEnumerable<Runner.CodeRunners.Models.Bot> runnerBots1 = g.Field.Bots[firstPlayer.Name].Select(mapper.Map<Runner.CodeRunners.Models.Bot>);

            IEnumerable<Runner.CodeRunners.Models.Bot> runnerBots2 = g.Field.Bots[secondPlayer.Name].Select(mapper.Map<Runner.CodeRunners.Models.Bot>);

            runner.RunCodeGame(compResult1.InformationForCodeRunner, compResult2.InformationForCodeRunner, field, runnerBots1, runnerBots2, finishCondition);

            runner.GameFinished += Runner_GameFinished;
        }

        private void Runner_GameFinished(object sender, GameFinishedEventArgs e)
        {
            FinishEventArgs finishEventArgs = new FinishEventArgs();
            finishEventArgs.WinnerName = e.WinnerName;
            finishEventArgs.Commands = new List<Command>();

            foreach (var command in e.Commands)
            {
                finishEventArgs.Commands.Add(new Command()
                {
                    PlayerName = command.PlayerName,
                    ActionType = command.ActionType.ToString(),
                    BotId = command.BotId,
                    StepParams = command.StepParams
                });
            }

            GameFinished?.Invoke(this, finishEventArgs);
        }

        public event EventHandler<FinishEventArgs> GameFinished;
    }
}
