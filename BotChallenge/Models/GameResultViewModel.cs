using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Models
{
    public class GameResultViewModel
    {
        public GameResultViewModel(string winnerName, IEnumerable<CommandViewModel> commands)
        {
            WinnerName = winnerName;
            Commands = commands;
        }

        public string WinnerName { get; }
        public IEnumerable<CommandViewModel> Commands { get; }
    }
}
