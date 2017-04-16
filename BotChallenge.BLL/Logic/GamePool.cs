using BotChallenge.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Logic
{
    public class GamePool
    {
        private Queue<Game> freeGames = new Queue<Game>();
        public List<Game> busyGames = new List<Game>();

        public Game CreateGame()
        {
            Game g = new Game();
            g.SubscribeOnThisGame((id1, id2) => StartGame());

            freeGames.Enqueue(g);
            return g;
        }

        public Game RegisterPlayer(Player player)
        {
            if (player.Game == null)
            {
                Game g = null;
                if (freeGames.Any())
                {
                    g = freeGames.Peek();
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

                return g;
            }

            return player.Game;
        }

        public void StartGame()
        {
            Game g = freeGames.Dequeue();
            g.IsReady = true;
            busyGames.Add(g);
        }
    }
}
