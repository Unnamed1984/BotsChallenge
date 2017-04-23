using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FSWatcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher fsWatcher = new FileSystemWatcher(AppDomain.CurrentDomain.BaseDirectory);

            fsWatcher.Changed += (sender, e) =>
            {
                Console.WriteLine(e.ChangeType);
                Console.WriteLine(e.FullPath);

                Console.WriteLine("File content");

                Console.WriteLine(File.ReadAllText(e.FullPath));
            };

            fsWatcher.EnableRaisingEvents = true;

            while (true) { }
        }
    }
}
