using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Models
{
    public class CommandViewModel
    {
        public CommandViewModel(String playerName, String botId, String actionType, String[] stepParams)
        {
            PlayerName = playerName;
            BotId = botId;
            ActionType = actionType;
            StepParams = stepParams;
        }

        public string PlayerName { get; set; }
        public string BotId { get; set; }
        public String ActionType { get; set; }
        public string[] StepParams { get; set; }
    }
}
