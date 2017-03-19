using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Models
{
    public class Bot
    {
        public Int32 Id { get; set; }
        public String Program { get; set; }
        public Int32 Health { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public void Compile()
        {
            // Here you should check Program's code with analysators
        }
    }
}
