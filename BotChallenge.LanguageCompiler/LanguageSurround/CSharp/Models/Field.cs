namespace Bots.Models
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

        public override bool Equals(object obj)
        {
            Field another = obj as Field;

            if (another == null || GetType() != another.GetType())
            {
                return false;
            }

            if (Width != another.Width || Height != another.Height)
            {
                return false;
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Points[i][j] != another.Points[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Points.GetHashCode();
        }
    }

    public enum Point
    {
        Empty,
        RedBot,
        BlueBot, 
        Obstacle
    };
}
