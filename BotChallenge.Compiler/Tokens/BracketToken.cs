using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    internal class BracketToken : Token
    {
        internal BracketToken() { }

        internal BracketToken(string bracket)
        {
            this.IsOpen = bracket == "(";
        }
        internal bool IsOpen { get; private set; }

        internal override string TokenType
        {
            get
            {
                return "bracket";
            }
        }

        internal static Token Parse(string code)
        {
            if (code == "(" || code == ")")
            {
                return new BracketToken(code);
            }

            return null;
        }

        public override string ToString()
        {
            return $"Bracket. Opened { this.IsOpen }";
        }
    }
}