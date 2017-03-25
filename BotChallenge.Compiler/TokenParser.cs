using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Tokens;

namespace BotChallenge.Compiler
{
    public class TokenParser
    {
        private Func<string, Token>[] tokenParsers = 
            {
                OperationToken.Parse,
                BraceToken.Parse,
                BracketToken.Parse,
                DigitToken.Parse,
                KeyWorldToken.Parse,
                MemberCallToken.Parse,
                VariableToken.Parse
            };

        public IEnumerable<Token> ParseCode(string code)
        {
            IEnumerable<string> splittedBySpace = this.Split(code, new char[] { ' ', ';', '\r', '\n' } , new char[] { '(', ')', ',', '.' });

            List<Token> tokens = new List<Token>();

            foreach (string token in splittedBySpace)
            {
                Token tokenObj = this.getToken(token);

                if (tokenObj != null)
                {
                    tokens.Add(tokenObj);
                }
                else
                {
                    throw new NotFoundTokenException($"Not found token for { token }");
                }
            }

            return tokens;
        }

        private Token getToken(string code)
        {
            foreach (var parser in tokenParsers)
            {
                Token token = parser(code);

                if (token != null)
                {
                    return token;
                }
            }

            return null;
        }

        private string[] Split(string code, char[] splitExclude, char[] splitInclude)
        {
            List<string> words = new List<string>();
            String appending = string.Empty;

            foreach(char c in code)
            {
                if (splitExclude.Contains(c))
                {
                    this.addWord(ref words, appending);
                    appending = string.Empty;
                }
                else if (splitInclude.Contains(c))
                {
                    this.addWord(ref words, appending);
                    appending = string.Empty;

                    this.addWord(ref words, c.ToString());
                }
                else
                {
                    appending += c;
                }
            }

            return words.ToArray();
        }

        private void addWord(ref List<string> words, string word)
        {
            if (word.Trim() != String.Empty)
            {
                words.Add(word);
            }
        }
    }
}
