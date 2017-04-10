using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Tokens;


namespace BotChallenge.Compiler.Rules
{
    /// <summary>
    /// Describes Language Rule for the system
    /// </summary>
    public class LanguageRule
    {
        /// <summary>
        /// Steps how to decomposite nonterminal
        /// </summary>
        private Step[] steps;

        /// <summary>
        /// Matching NonTerminal
        /// </summary>
        public NonTerminal Symbol { get; set; }

        public LanguageRule(NonTerminal symbol, Step[] steps)
        {
            this.steps = steps;
            this.Symbol = symbol;
        }

        public bool CanMatchRule(Token token, IMatcher matcher)
        {
            return steps[0].TestStep(token, matcher);
        }

        public void MatchRule(ref Queue<Token> tokens, IMatcher matcher)
        {
            foreach (Step step in steps)
            {
                step.MakeStep(ref tokens, matcher);
            }
        }
    }
}
