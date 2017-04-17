using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BotChallenge.Compiler.Compilers
{
    static class AdditionFileProvider
    {
        public static string[] LoadFiles(params string[] paths)
        {
            List<string> contents = new List<string>();

            //string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LanguageSurround");

            string basePath = @"D:\Files\Projects\BotChallenge\BotChallenge.LanguageCompiler\LanguageSurround\";

            for (int i = 0; i < paths.Length; i++)
            {
                contents.Add(File.ReadAllText(Path.Combine(basePath, paths[i])));
            }

            return contents.ToArray();
        }
    }
}
