using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.CompilerProvider;
using BotChallenge.Compiler;
using BotChallenge.Compiler.Compilers;

namespace BotChallenge.CompilerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string Bot1 = @"
                    namespace Bots.Core 
                    {
                        public class Bot1 : Bot 
                        {
                            public void Move() 
                            {
                                Lesch
                                base.Move(StepDirection.Top);
                            }
                        }
                    }
                ";

            //string Bot2 = @"
            //        namespace Bots.Core 
            //        {
            //            public class Bot2 : Bot 
            //            {
            //                public void Move() 
            //                {
            //                    base.Move(StepDirection.Left);
            //                }
            //            }
            //        }
            //    ";

            ICompilerProvider compProvider = new CompilerProvider();
            ICompiler compiler = compProvider.GetCompilerForLanguage(CompilerSupportedLanguages.CSharp);

            List<string> errors = null;
            bool compileResult = compiler.VerifyCode(TaskParameters.Build(2), out errors, Bot1);

            Console.WriteLine($"Compilation result - { compileResult }");

            if (errors.Count > 0)
            {
                Console.Error.WriteLine("Detected errors");
            }

            foreach (string error in errors)
            {
                Console.Error.WriteLine(error);
            }

        }
    }
}
