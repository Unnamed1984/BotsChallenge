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

namespace BotChallenge.Compiler.Compilers
{
    class CSharpCompiler : ICompiler
    {
        public bool VerifyCode(TaskParameters task, out List<string> errors, params string[] codeClasses)
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

            bool compileResult = compileCodeClasses(path, out errors, this.extendBotCodeWithCoreClasses(codeClasses));

            if (!compileResult)
            {
                return false;
            }

            return verifyTaskLogic(path, task, out errors);
        }

        private string[] extendBotCodeWithCoreClasses(string[] botCodeClasses)
        {
            string[] coreFileContents = AdditionFileProvider.LoadFiles(Path.Combine("CSharp", "BotCodeFile.cs"), Path.Combine("CSharp", "ProgramCodeFile.cs"));

            List<string> allClassContents = new List<string>();
            allClassContents.AddRange(coreFileContents);
            allClassContents.AddRange(botCodeClasses);

            return allClassContents.ToArray();
        } 

        private bool compileCodeClasses(string pathToAssembly, out List<string> errors, string[] codeClasses)
        {
            errors = new List<string>();

            CSharpCodeProvider cscp = new CSharpCodeProvider(new Dictionary<string, string>()
            { { "CompilerVersion", "v3.5" } });

            CompilerParameters parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, pathToAssembly);
            parameters.GenerateExecutable = true;

            CompilerResults results = cscp.CompileAssemblyFromSource(parameters, codeClasses);

            if (results.Errors.HasErrors)
            {
                errors = new List<string>(results.Errors.Cast<CompilerError>().Select(ce => $"Compiler error: { ce.Line } line { ce.Column } column -> { ce.ErrorText }"));
                return false;
            }

            return true;
        }

        private bool verifyTaskLogic(string assemblyPath, TaskParameters task, out List<string> errors)
        {
            errors = new List<string>();
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

            if (task.RequiredBots != botCount)
            {
                errors.Add("All bots classes couldn't be found. Please add some bots.");
                return false;
            }

            return true;
        }
    }
}
