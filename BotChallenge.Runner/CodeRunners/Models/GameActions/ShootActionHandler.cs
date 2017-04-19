using System;

namespace BotChallenge.Runner.CodeRunners.Models.GameActions
{
    /// <summary>
    /// Represents shoot action. Requires 3 params : shooter position (x, y) and shoot direction ( see ShootDirection enum in this file).
    /// </summary>
    class ShootActionHandler : IActionHandler
    {
        public Field ApplyStep(string[] stepParams, Field field)
        {
            Tuple<int, int, ShootDirection> parsedParams = parseAndVerifyParameters(stepParams, field);

            int botX = parsedParams.Item1;
            int botY = parsedParams.Item2;
            ShootDirection direction = parsedParams.Item3;

            Tuple<int, int> step = movePointByDirection(botX, botY, direction);

            int shootX = step.Item1, shootY = step.Item2;

            while (shootX >= 0 && shootX < field.Width && shootY >= 0 && shootY < field.Height)
            {
                if (field.Points[shootY][shootX] == Point.BlueBot || field.Points[shootY][shootX] == Point.RedBot)
                {
                    field.Points[shootY][shootX] = Point.Empty;
                    break;
                }

                if (field.Points[shootY][shootX] == Point.Obstacle)
                {
                    break;
                }

                step = movePointByDirection(shootX, shootY, direction);
                shootX = step.Item1;
                shootY = step.Item2;

            }

            return field;
        }

        private Tuple<int, int, ShootDirection> parseAndVerifyParameters(string[] input, Field field)
        {
            if (input.Length != 3)
            {
                throw new ArgumentException($"Shoot action requires 3 parameters - bot position (x, y) and shoot direction. But got { input.ToString() }");
            }

            int botX = int.Parse(input[0]);
            int botY = int.Parse(input[1]);
            ShootDirection direction = (ShootDirection)Enum.Parse(typeof(ShootDirection), input[2]);

            if (field.Points[botY][botX] != Point.BlueBot && field.Points[botY][botX] != Point.RedBot)
            {
                throw new ArgumentException("Point addresed by bot position doesn't containing any bot.");
            }

            return new Tuple<int, int, ShootDirection>(botX, botY, direction);
        }

        private Tuple<int, int> movePointByDirection(int x, int y, ShootDirection direction)
        {
            switch (direction)
            {
                case ShootDirection.Top:
                    y--;
                    break;
                case ShootDirection.Bottom:
                    y++;
                    break;
                case ShootDirection.Left:
                    x--;
                    break;
                case ShootDirection.Right:
                    x++;
                    break;
            }

            return new Tuple<int, int>(x, y);
        }
    }

    enum ShootDirection
    {
        Top, 
        Right, 
        Bottom, 
        Left
    }
}
