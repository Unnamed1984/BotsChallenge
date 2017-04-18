using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Runner.CodeRunners.Models;
using System.IO;

namespace BotChallenge.Runner.CodeRunners
{
    class CSharpRunner : IRunner
    {
        private static class MapWorker
        {
            public static void WriteFieldToFile(Stream stream, Field field)
            {
                StreamWriter sw = new StreamWriter(stream);

                sw.WriteLine($" { field.Width } ; { field.Height } ;");

                StringBuilder fieldLine = new StringBuilder();

                for (int i = 0; i < field.Height; i++)
                {
                    fieldLine = fieldLine.Clear();

                    for (int linePos = 0; linePos < field.Width; linePos++)
                    {
                        fieldLine.AppendFormat(" {0} ;", (int)field.Points[i][linePos]);
                    }

                    sw.WriteLine(fieldLine.ToString());
                }

                sw.Flush();
            }

            public static Field ReadFieldFromFile(Stream stream)
            {
                StreamReader sr = new StreamReader(stream);

                string startLine = sr.ReadLine();

                IEnumerable<string> splittedBySemicolon = startLine.Split(';').Where(s => !string.IsNullOrWhiteSpace(s));

                int width = Int32.Parse(splittedBySemicolon.First());
                int height = Int32.Parse(splittedBySemicolon.Last());

                Point[][] points = new Point[height][];

                for (int i = 0; i < height; i++)
                {
                    points[i] = new Point[width];

                    string line = sr.ReadLine();
                    IEnumerable<string> strPoints = line.Split(';').Where(s => !string.IsNullOrWhiteSpace(s));

                    int linePos = 0;

                    foreach (string strPoint in strPoints)
                    {
                        points[i][linePos] = (Point)Int32.Parse(strPoint);
                    }

                }

                return new Field(width, height, points);
            }

        }

        public string RunCodeGame(RunnerInformation player1Info, RunnerInformation player2Info, Field field)
        {
            CSharpRunnerInformation csRunInfo1 = player1Info as CSharpRunnerInformation;
            CSharpRunnerInformation csRunInfo2 = player2Info as CSharpRunnerInformation;

            if (csRunInfo1 == null || csRunInfo2 == null)
            {
                throw new ArgumentNullException("Runner Information cannot be null.");
            }

            if (csRunInfo1.PlayerName == csRunInfo2.PlayerName)
            {
                throw new ArgumentException("Player names must be different");
            }

            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CompilerTempFiles");

            string fileName = generateUniqueFileName(dirPath);

            using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create))
            {
                MapWorker.WriteFieldToFile(fs, field);

                StreamWriter sw = new StreamWriter(fs);
                sw.Write($" { csRunInfo1.PlayerName } ;");

                FileSystemWatcher watcher = new FileSystemWatcher(Path.Combine(dirPath, fileName));
                watcher.Changed += (sender, e) => 
                {
                    StreamReader sr = new StreamReader(new FileStream(e.FullPath, FileMode.Open));
                    //string lastLine = sr.ReadToEnd().Split();
                };
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
