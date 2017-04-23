using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.BLL.JsonLoad.MapParser;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.CompilerTest
{
    static class MapTest
    {
        public static void TestJsonLoad()
        {
            FieldBuilder builder = new FieldBuilder(@"D:\Projects\C#\BotsChallenge\Server\BotChallenge\Content\levels\map1.json");

            Field f = builder.GetFieldForRunner();

            f = builder.PlaceBots(f, new List<BotChallenge.BLL.Models.Bot>() { new BLL.Models.Bot(5, 6, "Bot1"), new BLL.Models.Bot(8, 2, "Bot2") }, 1);

            f = builder.PlaceBots(f, new List<BotChallenge.BLL.Models.Bot>() { new BLL.Models.Bot(9, 6, "Bot1"), new BLL.Models.Bot(8, 7, "Bot2") }, 2);

            printField(f);

            Console.ReadKey();
        }

        private static void printField(Field f)
        {
            Console.WriteLine($" Height -> { f.Height } ; Width -> { f.Width } ");

            for (int i = 0; i < f.Height; i++)
            {
                Console.WriteLine();

                for (int j = 0; j < f.Width; j++)
                {
                    Console.Write($" { (int)f.Points[i][j] } ; ");
                }
            }
        }
    }
}
