using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BotChallenge.BLL.Models;
using BotChallenge.BLL.Logic;
using System.Collections.Concurrent;

namespace SignalRMvc.Hubs
{
    public class GameHub : Hub
    {
        static List<Player> Users = new List<Player>();
        static GamePool pool = new GamePool();

        //// Переход на страницу ожидания
        //public void WaitingPage(String id)
        //{
        //    Clients.User(id).goForWaiting();
        //}

        // Переход на страницу игры
        public void GoToGamePage(String login1, String login2)
        {
            var firstPlayerConnections = Users.Single(u => u.Name == login1).ConnectionIds.ToList();
            var secondPlayerConnections = Users.Single(u => u.Name == login2).ConnectionIds.ToList();
            firstPlayerConnections.AddRange(secondPlayerConnections);

            Clients.Clients(firstPlayerConnections).goForGame();
        }


        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            var p = Users.FirstOrDefault(u => u.Name == userName);
            if (p == null)
            {
                Player p1 = new Player { Name = userName };
                p1.ConnectionIds = new HashSet<string>() { id };

                Users.Add(p1);

                // Посылаем сообщение текущему пользователю
                Clients.Caller.goForWaiting();

                Game g = pool.RegisterPlayer(p1);

                g.SubscribeOnThisGame(GoToGamePage);
            }
            else
            {
                if (p.Game.IsReady)
                {
                    Clients.Clients(Users.First(u => u.Name == userName).ConnectionIds.ToList()).goForGame();
                }
                else
                {
                    Clients.Clients(Users.First(u => u.Name == userName).ConnectionIds.ToList()).goForWaiting();
                }
            }
        }

        // Отмена ожидания
        public void CancelWaiting(string userName)
        {
            var user = Users.Single(u => u.Name == userName);
            Clients.Clients(user.ConnectionIds.ToList()).logOut();

            user.Game.Players.Remove(user);
            Users.Remove(user);
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(u => u.ConnectionIds.Contains(Context.ConnectionId));
            if (item != null)
            {
                //if (item.ConnectionIds.Count == 1)
                //{
                //    Clients.User(Context.ConnectionId).logOut();
                //    Users.Remove(item);
                //}

                item.ConnectionIds.Remove(Context.ConnectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}