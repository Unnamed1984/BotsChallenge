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
        private event Action FilledGameEvent;


        public String Id { get; set; }
        public List<Player> Players { get; set; }
        public Int32 String { get; set; }
        public Field Field { get; set; }

        #region Player's registration and notification

        public void RegisterPlayer(Player p)
        {
            if (this.Players.Count < 2)
            {
                this.Players.Add(p);

                if (this.Players.Count == 2)
                {
                    RaiseFilledGameEvent();
                }
            }
        }

        public void SubscribeOnThisGame(Action method) => FilledGameEvent += method;

        public void RaiseFilledGameEvent() => FilledGameEvent();

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
