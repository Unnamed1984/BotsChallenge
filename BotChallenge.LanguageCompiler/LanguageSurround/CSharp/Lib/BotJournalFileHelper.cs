using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bots.Models;

namespace Bots.Lib
{
    internal static class BotJournalFileHelper
    {
        public static void WriteFieldToFile(Stream stream, Field field)
        {
            StreamWriter sw = new StreamWriter(stream, Encoding.Default);

            sw.WriteLine(String.Format(" { 0 } ; { 1 } ;", field.Width, field.Height ));

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

        public static Field ReadFieldFromStream(Stream stream)
        {
            StreamReader sr = new StreamReader(stream, Encoding.Default);

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

        /// <summary>
        /// Process each line of commands part of boitJournal file. 
        /// Each next command line should be -> 'PlayerName BotId ActionType [ action parameters ] '
        /// </summary>
        /// <param name="line"> line to process </param>
        public static GameCommand ParseGameCommand(string line)
        {
            string[] lineParts = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // just player name in bot file
            if (lineParts.Length == 1)
            {
                return new GameCommand()
                {
                    PlayerName = lineParts[0]
                };
            }

            if (lineParts.Length < 3)
            {
                throw new ArgumentException("According to .botJournal specification there must be minimum 3 values separated by semicolon in each command line. That should be -> PlayerName, BotId, ActionType. Further can be specified action parameters.");
            }

            return new GameCommand()
            {
                PlayerName = lineParts[0],
                BotId = lineParts[1],
                ActionType = (GameAction)Enum.Parse(typeof(GameAction), lineParts[2]),
                StepParams = lineParts.Skip(3).ToArray()
            };
        }

    }
}
