using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.Runner.CodeRunners.Lib
{
    internal class BotJournalFileWatcher
    {
        private FileSystemWatcher fsWatcher;

        private Field field;
        private string lastLine;

        private string watchingFilePath;

        BotJournalFileWatcher(string directory, string fileName)
        {
            fsWatcher = new FileSystemWatcher(directory);
            fsWatcher.NotifyFilter = NotifyFilters.LastWrite;

            using (FileStream fs = new FileStream(Path.Combine(directory, fileName), FileMode.OpenOrCreate, FileAccess.Read))
            {
                field = MapWorker.ReadFieldFromStream(fs);
            }

            fsWatcher.Changed += FsWatcher_Changed;
            watchingFilePath = Path.Combine(directory, fileName);
        }

        /// <summary>
        /// Reacts to file change and process it. Changes can be 2 types: Field data edited, or last bot command (added/edited).
        /// If lastLine stays the same -> we update our field 
        /// Else we update last command
        /// </summary>
        /// <param name="sender">Request sender</param>
        /// <param name="e"> EventArgs for this kind of event. </param>
        private void FsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath != watchingFilePath)
            {
                return;
            }

            string fileContent;

            using (FileStream fs = new FileStream(watchingFilePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                fileContent = sr.ReadToEnd();
            }

            string[] lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (lastLine.Equals(lines.Last()))
            {
                using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(fileContent)))
                {
                    field = MapWorker.ReadFieldFromStream(ms);
                }
            }
            else
            {

            }

        }

        private void processLine(string line)
        {
        }
    }
}
