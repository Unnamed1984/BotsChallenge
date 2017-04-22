using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Bots.Core;
using Bots.Models;
using Bots.Lib;
using System.IO;
using System.Text;

namespace Bots.Run
{
    public static class Program
    {
        private static int botStepperIndex = 0;
        public static void Main(string[] args)
        {
            Console.WriteLine("Bot competition started");

            Assembly a = typeof(Program).Assembly;

            IEnumerable<Type> botTypes = a.GetExportedTypes().Where(t => t.BaseType == typeof(Bot));
            List<Bot> bots = new List<Bot>();

            foreach (Type botType in botTypes)
            {
                bots.Add((Bot)Activator.CreateInstance(botType));
            }

            string dirPath = args[0];
            string fileName = args[1];
            string playerName = args[2];
            Field f = null;

            Console.WriteLine("Before reading field");

            using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                f = BotJournalFileHelper.ReadFieldFromStream(fs);
            }

            Console.WriteLine("After reading field");

            BotJournalFileWatcher watcher = new BotJournalFileWatcher(dirPath, fileName);

            Console.WriteLine("Adding handlers");

            watcher.FieldEdited += (sender, e) => f = e.NewField;
            watcher.CommandEdited += (sender, e) => 
            {
                GameCommand command = e.NewCommand;
                global::System.Console.WriteLine("Game command playerName - " + command.PlayerName);
                if (!command.PlayerName.Trim().Equals(playerName) || command.BotId != null)
                {
                    return;
                }

                string[] stepArr = makeStep(bots, f);

                using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    writeCommandParams(fs, stepArr);
                }
            };

            Console.WriteLine("Handlers added");

            string lastLine = null;

            using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                string fileContent = sr.ReadToEnd();
                lastLine = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Last();
            }

            Console.WriteLine("File read to find last line - '" + lastLine + "'");

            string[] splittedLine = lastLine.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("Splitted arr - " + stringArrToString(splittedLine) + " Length - " + splittedLine.Length);

            if (splittedLine.Length == 1)
            {
                Console.WriteLine("Matched splitted line");
                if (splittedLine[0].Trim().Equals(playerName))
                {
                    string[] step = makeStep(bots, f);
                    Console.WriteLine("Step params");

                    using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        writeCommandParams(fs, step);
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Starting cycle");

            while (true) { }
        }

        private static string[] makeStep(List<Bot> bots, Field f)
        {
            Bot b = bots[botStepperIndex];
            botStepperIndex = (botStepperIndex + 1) % bots.Count;
            Step s = b.NextStep(f);
            List<string> stepList = new List<string>() { b.GetType().Name, s.Action.ToString() };
            stepList.AddRange(s.ToStringParameterArray());

            return stepList.ToArray();
        }

        private static void writeCommandParams(Stream s, string[] parameters)
        {
            StreamWriter sw = new StreamWriter(s);

            foreach (string param in parameters)
            {
                sw.Write(String.Format(" {0} ;", param));
                Console.Write(String.Format(" {0} ;", param));
            }

            sw.WriteLine();
            sw.Flush();
        }

        private static string stringArrToString(string[] arr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string a in arr)
            {
                sb.AppendFormat(" '{0}' ", a);
            }

            return sb.ToString();
        }
    }
}