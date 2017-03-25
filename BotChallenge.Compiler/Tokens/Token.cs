using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    public abstract class Token
    {
        internal abstract string TokenType { get; }
    }
}
