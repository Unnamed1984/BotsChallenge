using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Runner.CodeRunners.Models;
using System.IO;
using BotChallenge.Runner.CodeRunners.Lib;

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

            using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create))
            {
                MapWorker.WriteFieldToFile(fs, field);

                StreamWriter sw = new StreamWriter(fs);
                sw.Write($" { csRunInfo1.PlayerName } ;");
                sw.Flush();

                FileSystemWatcher watcher = new FileSystemWatcher(Path.Combine(dirPath, fileName));
                watcher.Changed += (sender, e) =>
                {
                    StreamReader sr = new StreamReader(new FileStream(e.FullPath, FileMode.Open));

                    string lastLine = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Last();
                    Console.WriteLine($"Runner last line '{ lastLine }' ");
                };
            }
                       
            return string.Empty;
        }

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
    }
}
