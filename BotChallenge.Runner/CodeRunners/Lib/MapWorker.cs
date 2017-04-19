using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners.Lib
{
    internal static class MapWorker
    {
        public static void WriteFieldToFile(Stream stream, Field field)
        {
            StreamWriter sw = new StreamWriter(stream, Encoding.Default);

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

    }
}
