using BotChallenge.BLL.Logic;
using BotChallenge.BLL.Models;
using BotChallenge.Models;
using BotChallenge.Util;
using Microsoft.AspNet.SignalR;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SignalRMvc.Hubs
{
    public class GameHub: Hub
    {
        private static BotNameMapper _nameMapper = new BotNameMapper(HttpContext.Current.Server.MapPath("~/CompilerTempFiles"));
        public void SetReadyForGame(String login1)
        {
            Player p = GameManager.FindUser(login1);
            p.Game.SetPlayerAsReady(p);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            var p = GameManager.FindUser(userName);

            p.ConnectionIds.Add(id);
            if (p != null)
            {
                if (!p.Game.IsSubscriptionOnGameStateOn())
                {
                    p.Game.SubscribeOnGameState(RunGame);
                }
            }
            else
            {
                Clients.Caller.displayUnauthorizedMessage();
            }
        }

        public void RunGame(String login1, String login2)
        {
            var firstPlayerConnections = GameManager.FindUser(login1).ConnectionIds.ToList();
            var secondPlayerConnections = GameManager.FindUser(login2).ConnectionIds.ToList();

            Clients.Clients(firstPlayerConnections).run(login1);
            Clients.Clients(secondPlayerConnections).run(login2);
        }

        public void RunGameLast(RunBotsModel model, String login)
        {
            Player p = GameManager.FindUser(login);

            p.BotsCode = model.Code.Select(m => m.Code).ToArray();
            p.BotsCount = model.BotsCount;

            Game game = p.Game;

            Dictionary<string, string> nameMap;
            lock (game)
            {
                nameMap = _nameMapper.CreateMappingForPlayer(model, p.Name, game.Id);
            }
            List<Bot> playerBots = game.Field.Bots[p.Name];
            game.Field.Bots[p.Name] = mapBotNames(playerBots, nameMap);

            if (game.Players.Any(pl => pl.BotsCode.Length == 0))
            {
                return;
            }

            lock (game)
            {
                if (!game.IsCodeExecuted)
                {
                    BotsRunner runner = new BotsRunner();

                    HttpContextBase httpContext = Context.Request.GetHttpContext();
                    runner.RunCode(game, httpContext.Server.MapPath("~/Content/levels/map1.json"), GameFinishType.CommandsNumber);

                    var connections = GameManager.FindUser(login).ConnectionIds.ToList();

                    runner.GameFinished += (s, e) =>
                    {
                        foreach (var command in e.Commands)
                        {
                            Dictionary<string, string> playerMapping = _nameMapper.GetMappingForPlayer(command.PlayerName, game.Id);
                            command.BotId = playerMapping.FirstOrDefault(n => n.Value.Equals(command.BotId)).Key;
                        }
                        Clients.Clients(connections).startGameMovie(e);
                    };
                    game.IsCodeExecuted = true;
                }
            }

        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = GameManager.FindUserByConnectionId(Context.ConnectionId);
            if (item != null)
            {
                //if (item.ConnectionIds.Count == 1)
                //{
                //    Clients.User(Context.ConnectionId).logOut();
                //    Users.Remove(item);
                //}

                Clients.Caller.disconnect(item.Name);
                item.ConnectionIds.Remove(Context.ConnectionId);
            }

            return base.OnDisconnected(stopCalled);
        }


        /// <summary>
        /// Replaces bot names according to map dictionary
        /// </summary>
        /// <param name="bots"> List of bots </param>
        /// <param name="mapDictionary"> Map dictionary </param>
        /// <returns> Updated bot list </returns>
        private List<Bot> mapBotNames(List<Bot> bots, Dictionary<string, string> mapDictionary)
        {
            foreach (Bot bot in bots)
            {
                bot.Name = mapDictionary[bot.Name];
            }
            return bots;
        }
    }
}
