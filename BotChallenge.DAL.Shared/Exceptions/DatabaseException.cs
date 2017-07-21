using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.DAL.Shared.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() : base() { }
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, Exception inner) : base(message, inner) { }
    }
}
