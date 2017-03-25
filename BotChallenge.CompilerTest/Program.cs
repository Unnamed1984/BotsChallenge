using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler;
using BotChallenge.Compiler.Tokens;

namespace BotChallenge.CompilerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenParser parser = new TokenParser();
            IEnumerable<Token> tokens = null;
            try
            {
                tokens = parser.ParseCode(
                    @" class Bot1 : Bot { 
                        StepDirection Step() {
                            if ( this.distanceTo(b2, b3) > 50) {
                                return StepDirection.Left;
                            }
                            return StepDirection.Top;
                        }
                    } ");
            }
            catch (NotFoundTokenException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }

            foreach (Token token in tokens)
            {
                Console.WriteLine(token.ToString());
            }  

        }
    }
}
