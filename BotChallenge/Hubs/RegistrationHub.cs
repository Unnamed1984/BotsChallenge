using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BotChallenge.BLL.Models;
using BotChallenge.BLL.Logic;
using System.Collections.Concurrent;
using BotChallenge.Util;

namespace SignalRMvc.Hubs
{
    public class RegistrationHub : Hub
    {
        //// Переход на страницу ожидания
        //public void WaitingPage(String id)
        //{
        //    Clients.User(id).goForWaiting();
        //}

        // Переход на страницу игры
        public void GoToGamePage(String login1, String login2)
        {
            var firstPlayerConnections = GameManager.FindUser(login1).ConnectionIds.ToList();
            var secondPlayerConnections = GameManager.FindUser(login2).ConnectionIds.ToList();

            Clients.Clients(firstPlayerConnections).goForGame(login1);
            Clients.Clients(secondPlayerConnections).goForGame(login2);

            GameManager.FindUser(login1).ConnectionIds.Clear();
            GameManager.FindUser(login2).ConnectionIds.Clear();
        }


        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            var p = GameManager.FindUser(userName);
            if (p == null)
            {
                Player p1 = new Player { Name = userName };
                p1.ConnectionIds = new HashSet<string>() { id };

                GameManager.AddUser(p1);

                // Посылаем сообщение текущему пользователю
                Clients.Caller.goForWaiting();

                Game g = GameManager.RegisterPlayer(p1);

                if (!g.IsSubscriptionOnGameOn())
                {
                    g.SubscribeOnThisGame(GoToGamePage);
                }
            }
            else
            {
                if (p.Game.IsReady)
                {
                    Clients.Clients(GameManager.FindUser(userName).ConnectionIds.ToList()).goForGame(p.Name);
                }
                else
                {
                    Clients.Clients(GameManager.FindUser(userName).ConnectionIds.ToList()).goForWaiting();
                }
            }
        }

        // Отмена ожидания
        public void CancelWaiting(string userName)
        {
            List<String> connectionIds = GameManager.UnregisterUser(userName);

            Clients.Clients(connectionIds).logOut();
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

                item.ConnectionIds.Remove(Context.ConnectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}