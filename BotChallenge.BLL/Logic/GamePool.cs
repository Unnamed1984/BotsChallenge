using BotChallenge.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Logic
{
    public static class GamePool
    {
        public static string MapPath = null;

        public static Queue<Game> FreeGames { get; private set; } = new Queue<Game>();
        public static List<Game> BusyGames { get; private set; } = new List<Game>();

        public static Game CreateGame()
        {
            Game g = new Game();
            //g.SubscribeOnThisGame((id1, id2) => StartGame());

            FreeGames.Enqueue(g);
            return g;
        }

        public static Game RegisterPlayer(Player player)
        {
            if (player.Game == null)
            {
                Game g = null;
                if (FreeGames.Count > 0)
                {
                    g = FreeGames.Dequeue();
                }
                else
                {
                    g = CreateGame();
                }

                g.RegisterPlayer(player);

                if (g.Players.Count == 1)
                {
                    g.Field.Bots.Add(player.Name, new List<Bot>(){
                        new Bot(3, 10, "R2D2"),
                        new Bot(10, 11, "CTripi-O"),
                        new Bot(24, 4, "UT-12"),
                        new Bot(23, 9, "XW-23-4")
                    });
                }
                else
                {
                    g.Field.Bots.Add(player.Name, new List<Bot>(){
                        new Bot(6, 2, "OI-12-A"),
                        new Bot(7, 5, "T34-T2"),
                        new Bot(21, 8, "OWIN41-X"),
                        new Bot(7, 11, "NT-98-D4")
                    });
                }
                g.Field.MapPath = GamePool.MapPath;

                if (g.Players.Count == 2)
                {
                    BusyGames.Add(g);
                }

                return g;
            }

            return player.Game;
        }

        public static void StartGame()
        {
            Game g = FreeGames.Dequeue();
            g.IsReady = true;
            BusyGames.Add(g);
        }
    }
}
