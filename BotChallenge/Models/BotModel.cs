using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Models
{
    public class BotModel
    {
        public String Id { get; set; }
        public String Code { get; set; }
        public Int32 Health { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public String Name { get; set; }
    }
}
