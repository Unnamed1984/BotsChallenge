using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Exceptions
{
    public class StepException : Exception
    {
        public StepException() : base() { }
        public StepException(string message) : base(message) { }
    }
}
