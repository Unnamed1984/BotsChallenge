﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Compilers.Models
{
    public class CompilationResult
    {
        public bool IsCodeCorrect { get; internal set; }
        public List<string> Errors { get; internal set; }
        public RunnerInformation InformationForCodeRunner { get; internal set; }
    }
}
