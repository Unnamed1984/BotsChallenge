using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    internal class BraceToken : Token
    {
        internal BraceToken() { }

        internal BraceToken(string brace)
        {
            this.IsOpen = brace == "{";
        }
        internal bool IsOpen { get; private set; }

        internal override string TokenType
        {
            get
            {
                return "brace";
            }
        }

        internal static Token Parse(string code)
        {
            if (code == "{" || code == "}")
            {
                return new BraceToken(code);
            }

            return null;
        }

        public override string ToString()
        {
            return $"Brace. Opened - { this.IsOpen }";
        }
    }
}
