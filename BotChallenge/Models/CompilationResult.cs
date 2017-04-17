using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Models
{
    public class CompilationResultModel
    {
        public bool IsCodeCorrect { get; internal set; }
        public List<string> Errors { get; internal set; }
    }
}
