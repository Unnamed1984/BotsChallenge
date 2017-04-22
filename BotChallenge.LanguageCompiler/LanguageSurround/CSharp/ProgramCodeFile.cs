using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Bots.Core;
using Bots.Models;
using Bots.Lib;
using System.IO;

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

            using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Open, FileAccess.Read))
            {
                f = BotJournalFileHelper.ReadFieldFromStream(fs);

                StreamReader sr = new StreamReader(fs);


            }

            BotJournalFileWatcher watcher = new BotJournalFileWatcher(dirPath, fileName);

            watcher.FieldEdited += (sender, e) => f = e.NewField;
            watcher.CommandEdited += (sender, e) => 
            {
                GameCommand command = e.NewCommand;
                if (!command.PlayerName.Equals(playerName) || command.BotId != null)
                {
                    return;
                }

                string[] stepArr = makeStep(bots, f);

                using (FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Append, FileAccess.Read))
                {
                    StreamWriter sw = new StreamWriter(fs);

                    foreach (string param in stepArr)
                    {
                        sw.Write(String.Format(" {0} ;", param));
                    }

                    sw.Flush();

                }
            };
        }

        private static string[] makeStep(List<Bot> bots, Field f)
        {
            Bot b = bots[botStepperIndex];
            botStepperIndex = (botStepperIndex + 1) % bots.Count;
            Step s = b.NextStep(f);
            List<string> stepList = new List<string>() { s.Action.ToString() };
            stepList.AddRange(s.ToStringParameterArray());

            return stepList.ToArray();
        }

        private static void writeCommandParams(Stream s, string[] parameters)
        {
            StreamWriter sw = new StreamWriter(s);

            foreach (string param in stepArr)
            {
                sw.Write(String.Format(" {0} ;", param));
            }

            sw.Flush();
        }
    }
}