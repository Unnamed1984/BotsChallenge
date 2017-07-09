using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Entities.ResultOfGame
{
    public class Command
    {
        public string PlayerName { get; set; }
        public string BotId { get; set; }
        public string ActionType { get; set; }
        public string[] StepParams { get; set; }
    }
}
