namespace Bots.Models
{
    public class GameCommand
    {
        public string PlayerName { get; set; }
        public string BotId { get; set; }
        public GameAction ActionType { get; set; }
        public string[] StepParams { get; set; }
    }
}
