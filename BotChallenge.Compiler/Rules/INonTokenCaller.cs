using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Rules
{
    interface INonTokenCaller
    {
        void Call(NonTerminal terminal);
    }
}
