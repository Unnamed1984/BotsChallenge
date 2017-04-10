using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Rules.LanguageRules
{
    public interface ILanguage
    {
        IEnumerable<LanguageRule> Rules { get; }
        LanguageRule StartRule { get; }
    }
}
