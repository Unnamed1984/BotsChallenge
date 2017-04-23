using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Runner.CodeRunners.Models;
using BotChallenge.Runner.CodeRunners.Models.GameActions;
using System.IO;
using BotChallenge.Runner.CodeRunners.Lib;
using System.Diagnostics;
using BotChallenge.Runner.CodeRunners.FinishGame;

namespace BotChallenge.Runner.CodeRunners
{
    class CSharpRunner : IRunner
    {
        public string RunCodeGame(RunnerInformation player1Info, RunnerInformation player2Info, Field field, IEnumerable<Bot> bots1, IEnumerable<Bot> bots2, FinishGameCondition finishCondition)
        {
            CSharpRunnerInformation csRunInfo1 = player1Info as CSharpRunnerInformation;
            CSharpRunnerInformation csRunInfo2 = player2Info as CSharpRunnerInformation;

            this.verifyRunParameters(csRunInfo1, csRunInfo2);

            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CompilerTempFiles");

            string fileName = generateUniqueFileName(dirPath);

            using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BotJournalFileHelper.WriteFieldToFile(fs, field);

                StreamWriter sw = new StreamWriter(fs);
                sw.Write($" { csRunInfo1.PlayerName } ;");
                sw.Flush();

                BotJournalFileWatcher botFileWatcher = new BotJournalFileWatcher(dirPath, fileName);

                Process player1Process = null, player2Process = null;

                botFileWatcher.CommandEdited += (sender, e) =>
                {
                    GameCommand command = e.NewCommand;

                    Console.WriteLine("File changed");

                    if (command.BotId == null)
                    {
                        return;
                    }

                    Console.WriteLine("user wrote full command");

                    try
                    {
                        IActionHandler actionHandler = ActionHandlersProvider.GetActionHandler(command.ActionType);
                        Field newField = actionHandler.ApplyStep(command.StepParams, field);

                        if (!newField.Equals(field))
                        {
                            field = newField;
                            fs.Position = 0;
                            BotJournalFileHelper.WriteFieldToFile(fs, field);
                        }
                    }
                    catch (ArgumentException)
                    { }

                    using (FileStream s = new FileStream(Path.Combine(dirPath, fileName), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        sw = new StreamWriter(s);

                        if (command.PlayerName.Trim().Equals(player1Info.PlayerName))
                        {
                            sw.Write($" { player2Info.PlayerName } ;");
                        }
                        else
                        {
                            sw.Write($" { player1Info.PlayerName } ;");
                        }
                        sw.Flush();
                    }

                    if (finishCondition.IsFinished(field, botFileWatcher.CommandCount))
                    {
                        player1Process.Kill();
                        player2Process.Kill();

                        raiseFinishGameEvent(Path.Combine(dirPath, fileName));

                        botFileWatcher.Dispose();
                    }

                    Console.WriteLine("user command processed");
                };

                player1Process = Process.Start(csRunInfo1.PathToExecutable, $" { dirPath } { fileName } { csRunInfo1.PlayerName } { generateStringParameterForBots(bots1) }");

                player2Process = Process.Start(csRunInfo2.PathToExecutable, $" { dirPath } { fileName } { csRunInfo2.PlayerName } { generateStringParameterForBots(bots2) } ");

            }

            return string.Empty;
        }

        public event EventHandler<GameFinishedEventArgs> GameFinished;

        private void verifyRunParameters(CSharpRunnerInformation csRunInfo1, CSharpRunnerInformation csRunInfo2)
        {
            if (csRunInfo1 == null || csRunInfo2 == null)
            {
                throw new ArgumentNullException("Runner Information cannot be null.");
            }

            if (csRunInfo1.PlayerName == csRunInfo2.PlayerName)
            {
                throw new ArgumentException("Player names must be different");
            }
        }

        private string generateUniqueFileName(string dirPath)
        {
            string fileName = null;

            lock (GetType())
            {
                fileName = Guid.NewGuid().ToString() + ".botJournal";
                while (File.Exists(Path.Combine(dirPath, fileName)))
                {
                    fileName = Guid.NewGuid().ToString() + ".botJournal";
                }
            }

            return fileName;
        }

        private void raiseFinishGameEvent(string filePath)
        {
            string fileContent = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                fileContent = sr.ReadToEnd();
            }

            string[] lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int fieldHeight = getHeightFromInitLine(lines.First());
            List<GameCommand> commands = new List<GameCommand>();

            for (int i = fieldHeight + 1; i < lines.Length; i++)
            {
                string line = lines[i];
                GameCommand command = BotJournalFileHelper.ParseGameCommand(line);

                if (command.BotId != null)
                {
                    commands.Add(command);
                }
            }

            // TODO: detect winner basing on last field state
            GameFinished?.Invoke(this, new GameFinishedEventArgs(null, commands));
        }

        private int getHeightFromInitLine(string line)
        {
            string[] lineParts = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            return int.Parse(lineParts.Last().Trim());
        }

        private string generateStringParameterForBots(IEnumerable<Bot> bots)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bot in bots)
            {
                sb.AppendFormat($" { bot.Name } { bot.X } { bot.Y } ");
            }

            return sb.ToString();
        }
    }
}
