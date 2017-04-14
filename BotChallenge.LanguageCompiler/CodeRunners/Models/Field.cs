using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.CodeRunners.Models
{
    public class Field
    {
        public Point[][] Points { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public enum Point
    {
        RedBot,
        BlueBot, 
        Obstacle
    };
}
