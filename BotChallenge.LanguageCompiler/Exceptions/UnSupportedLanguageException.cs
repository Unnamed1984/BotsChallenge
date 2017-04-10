using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.LanguageCompiler.Exceptions
{
    /// <summary>
    /// Exception, that generates if user requests compiler object for not supported langnguage. 
    /// </summary>
    public class UnSupportedLanguageException : CompilerException
    {
        public UnSupportedLanguageException() : base() { }
        public UnSupportedLanguageException(string message) : base(message) { }
    }
}
