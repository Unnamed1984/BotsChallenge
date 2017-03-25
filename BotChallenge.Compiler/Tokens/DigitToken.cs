using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    internal class DigitToken : Token
    {
        internal DigitToken() {}
        internal DigitToken(string value)
        {
            Digit = double.Parse(value);
        }

        internal double Digit { get; private set; }

        internal override string TokenType
        {
            get
            {
                return "digit";
            }
        }

        internal static Token Parse(string code)
        {
            Regex exp = new Regex(@"[\+\-]?[0-9][0-9]*(\.[0-9]+)?");
            Match match = exp.Match(code);

            if (match.Success && match.Value == code)
            {
                return new DigitToken(match.Value);
            }
            else
            {
                return null;
            }

        }

        public override string ToString()
        {
            return $"Digit - { this.Digit } ";
        }
    }
}
