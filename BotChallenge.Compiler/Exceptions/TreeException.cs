using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Exceptions
{
    public class TreeException : Exception
    {
        public TreeException() : base() { }
        public TreeException(string message) : base(message) { }
    }
}
