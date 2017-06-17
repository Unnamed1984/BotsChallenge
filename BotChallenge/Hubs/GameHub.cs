using BotChallenge.BLL.Logic;
using BotChallenge.BLL.Models;
using BotChallenge.Models;
using BotChallenge.Util;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRMvc.Hubs
{
    public class GameHub: Hub
    {
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
            // smth with model;
            //GameResultViewModel result = new GameResultViewModel("test1", new List<CommandViewModel>()
            //{
            //    new CommandViewModel("test1", "R2D2", "Move", new String[] { "14", "14", "4", "11" }),
            //                    new CommandViewModel("test2", "T34-T2", "Move", new String[] { "14", "14", "8", "6" })
            //});

            Player p = GameManager.FindUser(login);

            p.BotsCode = model.Code;
            p.BotsCount = model.BotsCount;

            Game game = p.Game;

            if (game.Players.Any(pl => pl.BotsCode.Length == 0))
            {
                return;
            }

            BotsRunner runner = new BotsRunner();

            runner.RunCode(game.Players.First().BotsCode, game.Players.Last().BotsCode, game.Field.MapPath, GameFinishType.CommandsNumber, game.Field.Bots[game.Players.First().Name], game.Field.Bots[game.Players.Last().Name]);

            var connections = GameManager.FindUser(login).ConnectionIds.ToList();

            // Clients.Clients(connections).startGameMovie(result);
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
    }
}
