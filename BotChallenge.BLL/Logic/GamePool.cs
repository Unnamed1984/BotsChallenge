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
        private static Queue<Game> freeGames;
        public static List<Game> busyGames;

        public static Game CreateGame()
        {
            Game g = new Game();
            g.SubscribeOnThisGame(() => StartGame());

            freeGames.Enqueue(g);
            return g;
        }

        public static String RegisterPlayer(Player player)
        {
            if (freeGames.Any())
            {
                Game g = freeGames.Peek();
                g.RegisterPlayer(player);
                return g.Id;
            }
            else
            {
                Game g = CreateGame();
                g.RegisterPlayer(player);
                return g.Id;
            }
        }

        public static void StartGame()
        {
            Game g = freeGames.Dequeue();
            busyGames.Add(g);
        }
    }
}
