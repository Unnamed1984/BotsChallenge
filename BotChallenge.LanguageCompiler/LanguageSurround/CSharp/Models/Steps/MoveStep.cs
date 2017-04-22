using Bots.Models;
using Bots.Core;

namespace Bots.Models.Steps
{
    class MoveStep : Step
    {
        private Bot stepper;
        public MoveStep(Bot b, int newX, int newY)
        {
            stepper = b;
            NewX = newX;
            NewY = newY;
            Action = GameAction.Move;
        }

        public int NewX { get; set; }
        public int NewY { get; set; }

        public override string[] ToStringParameterArray()
        {
            return new string[] { stepper.X.ToString(), stepper.Y.ToString(), NewX.ToString(), NewY.ToString() };
        }
    }
}
