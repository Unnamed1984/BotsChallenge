using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler
{
    public class NotFoundTokenException : Exception
    {
        public NotFoundTokenException() { }

        public NotFoundTokenException(string message)
        {
            this.Message = message;
        }

        public override string Message { get; }
    }
}
