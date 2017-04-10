using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Tokens;

namespace BotChallenge.Compiler.Rules
{
    public class Matcher : IMatcher
    {
        private IEnumerable<LanguageRule> languageRules = null;

        public Matcher(IEnumerable<LanguageRule> language)
        {
            languageRules = language;
        }

        public bool CanMatch(NonTerminal terminal, Token token)
        {
            LanguageRule workingNowRule = getRuleByTokenAndNonTerminal(terminal, token);
            return workingNowRule != null;
        }

        private LanguageRule getRuleByTokenAndNonTerminal(NonTerminal terminal, Token token)
        {
            IEnumerable<LanguageRule> terminalRules = languageRules.Where(lR => lR.Symbol == terminal);

            LanguageRule workingNowRule = null;

            foreach (LanguageRule rule in terminalRules)
            {
                if (rule.CanMatchRule(token, this))
                {
                    workingNowRule = rule;
                }
            }
            return workingNowRule;
        } 

        public void Match(NonTerminal terminal, ref Queue<Token> tokens)
        {
            LanguageRule workingNowRule = this.getRuleByTokenAndNonTerminal(terminal, tokens.Peek());
            workingNowRule.MatchRule(ref tokens, this);
        }
    }
}
