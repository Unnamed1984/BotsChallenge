using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Tokens;

namespace BotChallenge.Compiler.Rules
{
    public interface IMatcher
    {
        bool CanMatch(NonTerminal terminal, Token token);
        void Match(NonTerminal terminal, ref Queue<Token> tokens);
    }
}
