using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Entities.DTO
{
    public class CompilationResultDTO
    {
        public bool IsCodeCorrect { get; internal set; }
        public List<string> Errors { get; internal set; }
    }
}
