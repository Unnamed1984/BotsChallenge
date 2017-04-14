using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Compiler.CodeRunners.Models;
using System.IO;

namespace BotChallenge.Compiler.CodeRunners
{
    class CSharpRunner : IRunner
    {
        public string RunCodeGame(RunnerInformation player1Info, RunnerInformation player2Info, Field field)
        {
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CompilerTempFiles");

            string fileName = generateUniqueFileName(dirPath);

            using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create))
            {

            }
            
            return string.Empty;
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
