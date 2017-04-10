using System;
using System.Collections.Generic;
using System.Linq;
using BotChallenge.Compiler.Tokens;
using BotChallenge.Compiler.Rules.LanguageRules;

namespace BotChallenge.Compiler.Rules
{
    public class ProcessStepper
    {
        private IEnumerable<LanguageRule> rules;
        private LanguageRule startRule;

        public ProcessStepper(ILanguage language)
        {
            this.startRule = language.StartRule;
            this.rules = language.Rules;
        }

        public void ParseTokens(IEnumerable<Token> tokens, ILanguage language)
        {
            Queue<Token> queue = new Queue<Token>(tokens);

            startRule.MatchRule(ref queue, new Matcher(rules));
        }
    }
}
