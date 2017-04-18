﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.LanguageProviders;
using BotChallenge.Compiler;
using BotChallenge.Compiler.Compilers;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Runner.CodeRunners.Models;
using BotChallenge.Runner.LanguageProviders;
using BotChallenge.Runner.CodeRunners;

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

            string Bot2 = @"
                    namespace Bots.Core 
                    {
                        public class Bot2 : Bot 
                        {
                            public void Move() 
                            {
                                base.Move(StepDirection.Left);
                            }
                        }
                    }
                ";

            ILanguageProvider compProvider = new LanguageProvider();
            ICompiler compiler = compProvider.GetCompilerForLanguage(CompilerSupportedLanguages.CSharp);

            CompilationResult compileResult1 = compiler.VerifyCode(TaskParameters.Build(2), Bot1, Bot2);

            Console.WriteLine($"Compilation result - { compileResult1.IsCodeCorrect }");

            if (compileResult1.Errors.Count > 0)
            {
                Console.Error.WriteLine("Detected errors");
            }

            foreach (string error in compileResult1.Errors)
            {
                Console.Error.WriteLine(error);
            }

            CompilationResult compileResult2 = compiler.VerifyCode(TaskParameters.Build(2), Bot2, Bot1);

            IRunnerProvider runProvider = new RunnerProvider();
            IRunner runner = runProvider.GetRunnerForLanguage(RunnerSupportedLanguages.CSharp);

            Field field = generateField(15, 15);

            runner.RunCodeGame(compileResult1.InformationForCodeRunner, compileResult2.InformationForCodeRunner, field);
            Console.ReadKey();
        }

        private static Field generateField(int width, int height)
        {
            Field field = new Field();

            field.Width = width;
            field.Height = height;

            field.Points = new Point[height][];
            Random rand = new Random();

            for (int i = 0; i < height; i++)
            {
                field.Points[i] = new Point[field.Width];

                for (int j = 0; j < field.Width; j++)
                {
                    field.Points[i][j] = (Point)rand.Next(3);
                }
            }

            return field;
        }
    }
}
