
namespace BotChallenge.Runner.CodeRunners.Models
{
    public class Field
    {
        public Field() { }

        public Field(int width, int height, Point[][] points)
        {
            this.Width = width;
            this.Height = height;
            this.Points = points;
        }

        public Point[][] Points { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public enum Point
    {
        Empty,
        RedBot,
        BlueBot, 
        Obstacle
    };
}
