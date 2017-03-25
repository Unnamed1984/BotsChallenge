using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    internal class MemberCallToken : Token
    {
        internal MemberCallToken() { }

        internal MemberCallToken(string className, string methodName)
        {
            this.ClassName = className;
            this.MethodName = methodName;
        }

        internal string ClassName { get; private set; }
        internal string MethodName { get; private set; }
        internal override string TokenType
        {
            get
            {
                return "method call";
            }
        }

        internal static Token Parse(string code)
        {
            string[] splitByPoint = code.Split('.');

            if (splitByPoint.Length != 2)
            {
                return null;
            }

            string className = splitByPoint[0];
            string methodName = splitByPoint[1];

            return new MemberCallToken(className, methodName);
        }

        public override string ToString()
        {
            return $"Class member token - { this.ClassName }.{ this.MethodName }";
        }
    }
}
