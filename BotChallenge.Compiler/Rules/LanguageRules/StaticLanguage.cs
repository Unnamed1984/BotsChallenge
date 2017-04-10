using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Tokens;

namespace BotChallenge.Compiler.Rules.LanguageRules
{
    class StaticLanguage : ILanguage
    {
        public IEnumerable<LanguageRule> Rules
        {
            get
            {
                return new LanguageRule[]
                {
                    new LanguageRule(new NonTerminal("Class"), new Step[]
                    {
                        new Step(StepOptions.Required, (t, m) =>
                        {
                            KeyWorldToken classKeyWorld = t as KeyWorldToken;
                            return classKeyWorld != null && classKeyWorld.KeyWorld == "class";
                        }),
                        new Step(StepOptions.Required, (t, m) =>
                        {
                            VariableToken classNameToken = t as VariableToken;
                            return classNameToken != null;
                        }),
                        new Step(StepOptions.Possible, (t, m) =>
                        {
                            OperationToken extendsOperations = t as OperationToken;
                            return extendsOperations != null && extendsOperations.Operation == ":";
                        }, (t, m) =>
                        {
                            VariableToken baseClassName = t as VariableToken;
                            return baseClassName != null;
                        }),
                        new Step(StepOptions.Required, (t, m) =>
                        {
                            BraceToken token = t as BraceToken;
                            return token != null && token.IsOpen;
                        })
                    })
                };
                throw new NotImplementedException();
            }
        }

        public LanguageRule StartRule
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
