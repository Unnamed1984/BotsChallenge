using BotChallenge.BLL.Models.ResultOfGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Models
{
    public class Game
    {

        public Game()
        {
            this.Players = new List<Player>();
            this.Field = new Field();
            this.Id = Guid.NewGuid().ToString();
        }

        private event Action<String, String> FilledGameEvent;


        public String Id { get; set; }
        public List<Player> Players { get; set; }
        public Int32 String { get; set; }
        public Field Field { get; set; }
        public bool IsReady { get; set; }

        #region Player's registration and notification

        public void RegisterPlayer(Player p)
        {
            if (this.Players.Any())
            {
                if (this.Players[0].Name == p.Name)
                {
                    return;
                }
            }

            p.Game = this;

            if (this.Players.Count < 2)
            {
                this.Players.Add(p);

                if (this.Players.Count == 2)
                {
                    RaiseFilledGameEvent(this.Players[0].Name, this.Players[1].Name);
                }
            }
        }

        public void SubscribeOnThisGame(Action<String, String> method) => FilledGameEvent += method;

        public void RaiseFilledGameEvent(String pId1, String pId2) => FilledGameEvent(pId1, pId2);

        #endregion

        #region Bot's interaction

        public List<Command> Run()
        {
            // Here will be something with bot's interaction
            return new List<Command>();
        }
        #endregion
    }
}
