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
                if (freeGames.Any())
                {
                    Game g = freeGames.Peek();
                    g.RegisterPlayer(player);
                    return g;
                }
                else
                {
                    Game g = CreateGame();
                    g.RegisterPlayer(player);
                    return g;
                }
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
