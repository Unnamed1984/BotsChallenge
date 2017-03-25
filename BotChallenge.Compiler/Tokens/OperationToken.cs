using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Tokens
{
    internal class OperationToken : Token
    {
        private static string[] operations = { "+", "-", "*", "/", "<", ">", "==", "<=", ">=", "=<", "=>", ":", ",", "." };

        OperationToken() { }
        OperationToken(string operation)
        {
            this.Operation = operation;
        }

        internal string Operation { get; private set; }

        internal OperationType Type
        {
            get
            {
                if (new string[] { "+", "-", "*", "/" }.Contains(this.Operation))
                {
                    return OperationType.Arythmetic;
                }
                else if (new string[] { "<", ">", "==", "<=", ">=", "=<", "=>" }.Contains(this.Operation))
                {
                    return OperationType.Logic;
                }
                else if (this.Operation == ":")
                {
                    return OperationType.Extends;
                }
                else if (this.Operation == ",")
                {
                    return OperationType.CommaSeparator;
                }
                else if (this.Operation == ".")
                {
                    return OperationType.DotSeparator;
                }
                else
                {
                    return OperationType.None;
                }
            }
        }

        internal override string TokenType
        {
            get
            {
                return "operation";
            }
        }

        internal static Token Parse(string code)
        {
            if (operations.Contains(code))
            {
                return new OperationToken(code);
            }

            return null;
        }

        public override string ToString()
        {
            return $"operation - { this.Operation }";
        }
    }

    public enum OperationType
    {
        Arythmetic, Logic, Extends, CommaSeparator, DotSeparator, None
    }
}
