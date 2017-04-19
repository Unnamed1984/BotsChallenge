using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Models
{
    public class Bot
    {
        public Bot(Int32 x , Int32 y, String name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.X = x;
            this.Y = y;
            this.Name = name;
        }

        public String Id { get; set; }
        public String Code { get; set; }
        public Int32 Health { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public String Name { get; set; }

        public void Compile()
        {
            // Here you should check Program's code with analysators
        }
    }
}
