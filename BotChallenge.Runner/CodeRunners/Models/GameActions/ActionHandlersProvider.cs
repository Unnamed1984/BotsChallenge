using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Runner.CodeRunners.Models.GameActions
{
    static class ActionHandlersProvider
    {
        static IActionHandler GetActionHandler(GameAction action)
        {
            if (action == GameAction.Move)
            {
                return new MoveActionHandler();
            }

            if (action == GameAction.Shoot)
            {
                return new ShootActionHandler();
            }

            throw new ArgumentException($"Invalid value of GameAction enumeration. Received value -> { action.ToString() }");
        }
    }
}
