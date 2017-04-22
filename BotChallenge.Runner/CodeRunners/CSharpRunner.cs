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

namespace BotChallenge.Runner.CodeRunners
{
    class CSharpRunner : IRunner
    {
        public string RunCodeGame(RunnerInformation player1Info, RunnerInformation player2Info, Field field)
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

                    if (botFileWatcher.CommandCount > 10)
                    {
                        raiseFinishGameEvent(Path.Combine(dirPath, fileName));

                        player1Process.Kill();
                        player2Process.Kill();

                        botFileWatcher.Dispose();
                    }

                    Console.WriteLine("user command processed");
                };

                player1Process = Process.Start(csRunInfo1.PathToExecutable, $" { dirPath } { fileName } { csRunInfo1.PlayerName }");
                player2Process = Process.Start(csRunInfo2.PathToExecutable, $" { dirPath } { fileName } { csRunInfo2.PlayerName }");

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
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            this.moveToCommandsPart(ref fs);

            StreamReader sr = new StreamReader(fs);

            List<GameCommand> commands = new List<GameCommand>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line != null && !string.IsNullOrWhiteSpace(line))
                {
                    commands.Add(BotJournalFileHelper.ParseGameCommand(line));
                }
            }

            fs.Dispose();

            // TODO: detect winner basing on last field state
            GameFinished?.Invoke(this, new GameFinishedEventArgs(null, commands));
        }

        private void moveToCommandsPart(ref FileStream s)
        {
            StreamReader sr = new StreamReader(s);

            string firstLine = sr.ReadLine();

            string[] splittedBySemicolon = firstLine.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            int height = int.Parse(splittedBySemicolon[1]);

            // reading all field lines
            for (int i = 0; i < height; i++)
            {
                sr.ReadLine();
            }
        }
    }
}
