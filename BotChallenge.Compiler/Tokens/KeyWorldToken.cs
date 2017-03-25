using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    internal class KeyWorldToken : Token
    {
        private static string[] worlds = { "class", "void", "base", "this", "if", "return" };

        internal KeyWorldToken() { }
        internal KeyWorldToken(string world)
        {
            this.KeyWorld = world;
        }
        
        internal string KeyWorld { get; private set; }

        internal override string TokenType
        {
            get
            {
                return "Key world";
            }
        }

        internal static Token Parse(string code)
        {
            if (KeyWorldToken.worlds.Contains(code))
            {
                return new KeyWorldToken(code);
            }

            return null;
        }

        public override string ToString()
        {
            return $"Key World - { this.KeyWorld }";
        }
    }
}
