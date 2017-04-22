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
                if (!_command.PlayerName.Equals(value.PlayerName))
                {
                    commandCount++;
                }
                _command = value;
            }
        }
        private string lastLine;

        private string watchingFilePath;

        internal BotJournalFileWatcher(string directory, string fileName)
        {
            using (FileStream fs = new FileStream(Path.Combine(directory, fileName), FileMode.OpenOrCreate, FileAccess.Read))
            {
                field = BotJournalFileHelper.ReadFieldFromStream(fs);
            }
            
            fsWatcher = new FileSystemWatcher(directory);
            fsWatcher.NotifyFilter = NotifyFilters.LastWrite;

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
                    field = BotJournalFileHelper.ReadFieldFromStream(ms);
                }
                if (FieldEdited != null)
                {
                    FieldEdited.Invoke(this, new FieldChangedEventArgs(this.field, field));
                }
            }
            else
            {
                GameCommand command = BotJournalFileHelper.ParseGameCommand(lines.Last());

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
