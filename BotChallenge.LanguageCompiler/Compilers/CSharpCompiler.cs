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

namespace BotChallenge.LanguageCompiler.Compilers
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

            bool compileResult = compileCodeClasses(path, out errors, codeClasses);

            if (!compileResult)
            {
                return false;
            }

            return verifyTaskLogic(path, task, out errors);
        }

        private bool compileCodeClasses(string pathToAssembly, out List<string> errors, string[] codeClasses)
        {
            errors = new List<string>();

            CSharpCodeProvider cscp = new CSharpCodeProvider(new Dictionary<string, string>()
            { { "CompilerVersion", "v4.5" } });

            CompilerParameters parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, pathToAssembly);
            parameters.GenerateExecutable = true;

            CompilerResults results = cscp.CompileAssemblyFromSource(parameters, codeClasses);

            if (results.Errors.HasErrors)
            {
                errors = new List<string>(results.Errors.Cast<CompilerError>().Select(ce => $"Compiler error: { ce.Line } line { ce.Column } column -> { ce.ErrorText }"));
                return results.Errors.HasErrors;
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
