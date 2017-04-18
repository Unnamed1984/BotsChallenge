using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Runner.Exceptions
{
    /// <summary>
    /// Exception, that generates if user requests compiler object for not supported langnguage. 
    /// </summary>
    public class UnSupportedLanguageException : Exception
    {
        public UnSupportedLanguageException() : base() { }
        public UnSupportedLanguageException(string message) : base(message) { }
    }
}
