using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.LanguageProviders;
using BotChallenge.Compiler;
using BotChallenge.Compiler.Compilers;
using BotChallenge.Compiler.Compilers.Models;

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

    ILanguageProvider compProvider = new LanguageProvider();
            ICompiler compiler = compProvider.GetCompilerForLanguage(CompilerSupportedLanguages.CSharp);

            CompilationResult compileResult = compiler.VerifyCode(TaskParameters.Build(2), Bot1);

            Console.WriteLine($"Compilation result - { compileResult.IsCodeCorrect }");

            if (compileResult.Errors.Count > 0)
            {
                Console.Error.WriteLine("Detected errors");
            }

            foreach (string error in compileResult.Errors)
            {
                Console.Error.WriteLine(error);
            }

        }
    }
}
