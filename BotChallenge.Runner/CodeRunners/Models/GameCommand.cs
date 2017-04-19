using BotChallenge.Runner.CodeRunners.Models.GameActions;

namespace BotChallenge.Runner.CodeRunners.Models
{
    public class GameCommand
    {
        public string PlayerName { get; set; }
        public string BotId { get; set; }
        public GameAction ActionType { get; set; }
        public string[] stepParams { get; set; }
    }
}
