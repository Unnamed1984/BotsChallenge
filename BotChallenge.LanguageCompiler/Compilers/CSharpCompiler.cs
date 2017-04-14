using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using BotChallenge.Compiler.Compilers.Models;

namespace BotChallenge.Compiler.Compilers
{
    class CSharpCompiler : ICompiler
    {
        public CompilationResult VerifyCode(TaskParameters task, params string[] codeClasses)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CompilerTempFiles", Guid.NewGuid().ToString() + ".exe");

            // verife that such file doesn't exists.
            lock (GetType())
            {
                while (File.Exists(path))
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CompilerTempFiles", Guid.NewGuid().ToString() + ".exe");
                }
            }

            CompilationResult compileResult = compileCodeClasses(path, this.extendBotCodeWithCoreClasses(codeClasses));

            if (!compileResult.IsCodeCorrect)
            {
                return compileResult;
            }

            bool taskCompliance = verifyTaskLogic(path, task);

            if (!taskCompliance)
            {
                compileResult.Errors = compileResult.Errors ?? new List<string>();
                compileResult.Errors.Add("You haven't declared classes for all required bots. Please add them.");
            }

            return compileResult;
        }

        private string[] extendBotCodeWithCoreClasses(string[] botCodeClasses)
        {
            string[] coreFileContents = AdditionFileProvider.LoadFiles(Path.Combine("CSharp", "BotCodeFile.cs"), Path.Combine("CSharp", "ProgramCodeFile.cs"));

            List<string> allClassContents = new List<string>();
            allClassContents.AddRange(coreFileContents);
            allClassContents.AddRange(botCodeClasses);

            return allClassContents.ToArray();
        } 

        private CompilationResult compileCodeClasses(string pathToAssembly, string[] codeClasses)
        {

            CSharpCodeProvider cscp = new CSharpCodeProvider(new Dictionary<string, string>()
            { { "CompilerVersion", "v4.0" } });

            CompilerParameters parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, pathToAssembly);
            parameters.GenerateExecutable = true;

            CompilerResults compilationResults = cscp.CompileAssemblyFromSource(parameters, codeClasses);

            CompilationResult returnResult = new CompilationResult();
            returnResult.InformationForCodeRunner = new CSharpRunnerInformation(pathToAssembly);

            if (compilationResults.Errors.HasErrors)
            {
                returnResult.Errors = new List<string>(compilationResults.Errors.Cast<CompilerError>().Select(ce => $"Compiler error: { ce.Line } line { ce.Column } column -> { ce.ErrorText }"));
                returnResult.IsCodeCorrect = false;

                return returnResult;
            }

            returnResult.IsCodeCorrect = true;
            return returnResult;
        }

        private bool verifyTaskLogic(string assemblyPath, TaskParameters task)
        {
            Assembly assembly = Assembly.LoadFile(assemblyPath);

            Type[] types = assembly.GetExportedTypes();
            int botCount = 0;

            Regex exp = new Regex(@"Bot[\d]+");

            foreach (Type t in types)
            {
                if (exp.IsMatch(t.FullName) && t.GetMethods().FirstOrDefault(m => m.Name == "Move") != null)
                {
                    botCount++;
                }
            }

            return task.RequiredBots == botCount;
        }
    }
}
