using System;
using System.Collections.Generic;
using BotChallenge.Compiler.Tokens;
using BotChallenge.Compiler.Exceptions;

namespace BotChallenge.Compiler.Rules
{
    public class Step
    {
        private Func<Token, IMatcher, bool>[] functions = null;

        private StepOptions stepMode = StepOptions.Required;

        public Step(StepOptions mode = StepOptions.Required, params Func<Token, IMatcher, bool>[] functions)
        {
            this.functions = functions;
            this.stepMode = mode;
        }

        public bool TestStep(Token token, IMatcher matcher)
        {
            if (functions != null)
            {
                return functions[0](token, matcher);
            }

            return false;
        }

        public void MakeStep(ref Queue<Token> tokens, IMatcher matcher)
        {
            switch (this.stepMode)
            {
                case StepOptions.Required:

                    this.makeRequiredStep(ref tokens, matcher);
                    break;

                case StepOptions.Possible:

                    this.makePossibleStep(ref tokens, matcher);
                    break;

                case StepOptions.OneOfMany:

                    this.makeOneOfManyStep(ref tokens, matcher);
                    break;
            }
        }

        private void callFunctionsArray(ref Queue<Token> tokens, IMatcher matcher)
        {
            foreach (var function in functions)
            {
                Token token = tokens.Dequeue();
                bool result = function(token, matcher);

                if (!result)
                {
                    throw new StepException($"Impossible to make step at token { token.ToString() }");
                }
            }
        }

        private void makeRequiredStep(ref Queue<Token> tokens, IMatcher matcher)
        {
            if (functions == null && functions.Length == 0)
            {
                throw new StepException("No functions declared for this rule");
            }

            this.callFunctionsArray(ref tokens, matcher);
        }

        private void makePossibleStep(ref Queue<Token> tokens, IMatcher matcher)
        {
            if (functions == null || functions.Length == 0)
            {
                throw new StepException("No functions declared for this rule");
            }

            Token first = tokens.Peek();

            if (!functions[0](first, matcher))
            {
                return;
            }

            this.callFunctionsArray(ref tokens, matcher);
        }

        private void makeOneOfManyStep(ref Queue<Token> tokens, IMatcher matcher)
        {
            if (functions == null || functions.Length == 0)
            {
                throw new StepException("No functions declared for this rule");
            }

            Token token = tokens.Dequeue();

            foreach (var function in functions)
            {
                if (function(token, matcher))
                {
                    return;
                }
            }
            // if we reach here then no one function worked well with that token
            throw new StepException($"Impossible to make step at token { token.ToString() }");
        }
    }

    public enum StepOptions
    {
        Required, Possible, OneOfMany
    }
}
