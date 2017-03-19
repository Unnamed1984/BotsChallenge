using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Models
{
    public class Field
    {
        public Point[][] Points { get; set; }
        public List<Bot> Bots { get; set; }
    }
}
