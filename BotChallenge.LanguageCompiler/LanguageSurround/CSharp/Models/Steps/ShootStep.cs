using Bots.Models;
using Bots.Core;

namespace Bots.Models.Steps
{
    class ShootStep : Step 
    {
        private Bot stepper;
        public ShootStep(Bot b, Direction direction)
        {
            stepper = b;
            Direction = direction;
            Action = GameAction.Shoot;
        }

        public Direction Direction { get; set; }

        public override string[] ToStringParameterArray()
        {
            return new string[] { stepper.X.ToString(), stepper.Y.ToString(), Direction.ToString() };
        }
    }
}
