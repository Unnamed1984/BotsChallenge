using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Rules
{
    public class NonTerminal
    {
        public NonTerminal(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            NonTerminal another = obj as NonTerminal;
            return this.Name == another.Name;
        }

        public static bool operator == (NonTerminal t1, NonTerminal t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(NonTerminal t1, NonTerminal t2)
        {
            return !(t1 == t2);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
