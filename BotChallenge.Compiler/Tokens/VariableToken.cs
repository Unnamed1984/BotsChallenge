using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    internal class VariableToken : Token
    {
        internal VariableToken() { }
        internal VariableToken(string name)
        {
            VariableName = name;
        }

        internal string VariableName { get; private set; }

        internal override string TokenType
        {
            get
            {
                return "variable";
            }
        }

        internal static Token Parse(string code)
        {
            Regex exp = new Regex("[a-zA-Z][a-zA-Z0-9]*");

            Match match = exp.Match(code);

            if (match.Success && match.Value == code)
            {
                return new VariableToken(match.Value);
            }
            else
            {
                return null;
            }

        }

        public override string ToString()
        {
            return $"Variable - { this.VariableName }";
        }
    }
}
