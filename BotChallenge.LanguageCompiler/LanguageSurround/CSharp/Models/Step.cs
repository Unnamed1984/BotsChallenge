namespace Bots.Models
{
    public abstract class Step
    {
        public GameAction Action { get; set; }
        public abstract string[] ToStringParameterArray();
    }
}
