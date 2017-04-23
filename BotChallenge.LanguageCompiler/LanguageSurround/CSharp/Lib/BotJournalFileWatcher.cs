using System;
using System.Linq;
using System.Text;
using System.IO;
using Bots.Models;

namespace Bots.Lib
{
    internal class BotJournalFileWatcher : IDisposable
    {
        private FileSystemWatcher fsWatcher;

        private Field field;

        private GameCommand _command;
        private int commandCount;

        private GameCommand command
        {
            get
            {
                return _command;
            }
            set
            {
                if (_command == null || _command.PlayerName == null || !_command.PlayerName.Equals(value.PlayerName))
                {
                    commandCount++;
                }
                _command = value;
            }
        }
        private string lastLine = "";

        private string watchingFilePath = "";

        internal BotJournalFileWatcher(string directory, string fileName)
        {
            using (FileStream fs = new FileStream(Path.Combine(directory, fileName), FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                field = BotJournalFileHelper.ReadFieldFromStream(fs);
            }
            
            fsWatcher = new FileSystemWatcher(directory);
            fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fsWatcher.EnableRaisingEvents = true;

            fsWatcher.Changed += FsWatcher_Changed;
            watchingFilePath = Path.Combine(directory, fileName);

            Console.WriteLine("Watching file - " + watchingFilePath);
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
            Console.WriteLine("file changed");

            if (e.FullPath != watchingFilePath)
            {
                return;
            }

            Console.WriteLine("Our file changed");

            string fileContent;

            using (FileStream fs = new FileStream(watchingFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                fileContent = sr.ReadToEnd();
            }

            string[] lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (lastLine.Equals(lines.Last()))
            {
                using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(fileContent)))
                {
                    field = BotJournalFileHelper.ReadFieldFromStream(ms);
                }

                Console.WriteLine("field edited");
                Console.WriteLine();

                if (FieldEdited != null)
                {
                    FieldEdited.Invoke(this, new FieldChangedEventArgs(this.field, field));
                }
            }
            else
            {
                GameCommand command = BotJournalFileHelper.ParseGameCommand(lines.Last());

                Console.WriteLine("command edited");
                Console.WriteLine();

                if (CommandEdited != null)
                {
                    CommandEdited.Invoke(this, new CommandChangedEventArgs(this.command, command));
                }

                this.command = command;
                lastLine = lines.Last();
            }

        }

        public event EventHandler<CommandChangedEventArgs> CommandEdited;
        public event EventHandler<FieldChangedEventArgs> FieldEdited;

        public int CommandCount
        {
            get
            {
                return commandCount;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    field = null;
                    _command = null;
                }

                // освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                fsWatcher.Dispose();

                disposedValue = true;
            }
        }

        ~BotJournalFileWatcher()
        {
            Dispose(false);
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
