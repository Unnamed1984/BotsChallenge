﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotChallenge.Models
{
    public class RunBotsModel
    {
        public CompilationBotsModel[] Code { get; set; }
        public Int32 BotsCount { get; set; }
    }
}