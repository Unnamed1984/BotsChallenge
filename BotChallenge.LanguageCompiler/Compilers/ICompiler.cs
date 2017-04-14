﻿using BotChallenge.Compiler.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Compilers.Models;

namespace BotChallenge.Compiler
{
    public interface ICompiler
    {
        CompilationResult VerifyCode(TaskParameters task, params string[] codeClasses);
    }
}
