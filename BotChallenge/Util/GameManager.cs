using BotChallenge.BLL.Logic;
using BotChallenge.BLL.Models;
using BotChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Util
{
    public static class GameManager
    {
        public static List<Player> Users = new List<Player>();
        public static GamePool pool = new GamePool();

        public static void AddUser(Player p)
        {
            Users.Add(p);
        }

        public static Player FindUser(String login)
        {
            return Users.FirstOrDefault(u => u.Name == login);
        }

        public static Player FindUserByConnectionId(String userId)
        {
            return Users.FirstOrDefault(u => u.ConnectionIds.Contains(userId));
        }

        public static BLL.Models.Game RegisterPlayer(Player p1)
        {
            return pool.RegisterPlayer(p1);
        }

        public static List<String> UnregisterUser(String login)
        {
            var user = GameManager.FindUser(login);

            user.Game.Players.Remove(user);
            Users.Remove(user);

            return user.ConnectionIds.ToList();
        }

        public static GameState GetGameState(String gameId)
        {
            // pool.GetGame(gameId);
            return null;
        }

        public static GameState GetGameStateForPlayer(String login)
        {
            Game game = GameManager.FindUser(login).Game;

            if (game == null)
            {
                return null;
            }

            return new GameState(game, login);
        }
    }
}
