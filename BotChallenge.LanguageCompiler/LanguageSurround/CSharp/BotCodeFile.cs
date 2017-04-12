using System;

namespace Bots.Core
{
    public class Bot
    {
        private readonly string pathToFieldFile = "|FIELD_FILE|"; // will be replaced by real path before compilation

        protected int x = 0;
        protected int y = 0;

        protected int maxX = 90;
        protected int maxY = 90;

        public double distanceTo(Bot b2)
        {
            return 10;
        }

        public void Move(StepDirection step)
        {
            switch (step)
            {
                case StepDirection.Left:
                    if (x > 0)
                    {
                        x--;
                    }
                    break;

                case StepDirection.Right:
                    if (x < maxX)
                    {
                        x++;
                    }
                    break;

                case StepDirection.Top:

                    if (y > 0)
                    {
                        y--;
                    }

                    break;

                case StepDirection.Bottom:

                    if (y < maxY)
                    {
                        y++;
                    }

                    break;
            }
        }
    }

    public enum StepDirection
    {
        Left, Top, Right, Bottom
    }
}