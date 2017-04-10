using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Exceptions
{
    public class InvalidDeclarationException : Exception
    {
        public InvalidDeclarationException() : base() { }
        public InvalidDeclarationException(string message) : base(message) { }
    }
}
