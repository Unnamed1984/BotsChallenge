using System;
using BotChallenge.Compiler.LanguageProviders;
using BotChallenge.Compiler;
using BotChallenge.Compiler.Compilers;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Runner.CodeRunners.Models;
using BotChallenge.Runner.LanguageProviders;
using BotChallenge.Runner.CodeRunners;
using System.Text;

namespace BotChallenge.CompilerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string Bot1 = @"
                    using Bots.Models.Steps;
                    using Bots.Models;
                    namespace Bots.Core 
                    {
                        public class Bot1 : Bot 
                        {
                            public override Step NextStep(Field f) 
                            {
                                return new MoveStep(this, this.X + 1, this.Y);
                            }
                        }
                    }
                ";

            string Bot2 = @"
                    using Bots.Models.Steps;
                    using Bots.Models;
                    namespace Bots.Core 
                    {
                        public class Bot2 : Bot 
                        {
                            public override Step NextStep(Field f) 
                            {
                                return new MoveStep(this, this.X, this.Y + 1);
                            }
                        }
                    }
                ";

            ILanguageProvider compProvider = new LanguageProvider();
            ICompiler compiler = compProvider.GetCompilerForLanguage(CompilerSupportedLanguages.CSharp);

            CompilationResult compileResult1 = compiler.CompileCode(TaskParameters.Build(2), Bot1, Bot2);

            Console.WriteLine($"Compilation result - { compileResult1.IsCodeCorrect }");

            if (compileResult1.Errors.Count > 0)
            {
                Console.Error.WriteLine("Detected errors");
            }

            foreach (string error in compileResult1.Errors)
            {
                Console.Error.WriteLine(error);
            }

            CompilationResult compileResult2 = compiler.CompileCode(TaskParameters.Build(2), Bot2, Bot1);

            if (compileResult1.IsCodeCorrect && compileResult2.IsCodeCorrect)
            {

                IRunnerProvider runProvider = new RunnerProvider();
                IRunner runner = runProvider.GetRunnerForLanguage(RunnerSupportedLanguages.CSharp);

                Field field = generateField(15, 15);

                compileResult1.InformationForCodeRunner.PlayerName = "player1";
                compileResult2.InformationForCodeRunner.PlayerName = "player2";

                runner.RunCodeGame(compileResult1.InformationForCodeRunner, compileResult2.InformationForCodeRunner, field);
                runner.GameFinished += Runner_GameFinished;
            }

            Console.ReadKey();
        }

        private static void Runner_GameFinished(object sender, GameFinishedEventArgs e)
        {
            Console.Clear();

            Console.WriteLine("Game finished");

            foreach (GameCommand command in e.Commands)
            {
                Console.WriteLine($" { command.PlayerName } ; { command.BotId } ; { command.ActionType } { stringArrToString(command.StepParams) } ");
            }
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

        private static string stringArrToString(string[] arr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string a in arr)
            {
                sb.AppendFormat("{0} ;", a);
            }

            return sb.ToString();
        }
    }
}
