using System.Collections.Generic;
using System.Linq;

namespace code_challenge
{
    public class MatchedBracketStringValidator
    {
        private static readonly Dictionary<char, char> BracketPairs = new()
        {
            { '[', ']' },
            { '(', ')' },
            { '{', '}' }
        };

        private static readonly List<char> OpenBrackets = BracketPairs.Keys.ToList();
        private static readonly List<char> CloseBrackets = BracketPairs.Values.ToList();

        public string ErrorMessage { get; private set; } = string.Empty;

        public bool IsValid(string input)
        {
            var openBracketStack = new Stack<char>();
            foreach (var charItem in input)
            {
                if (OpenBrackets.Contains(charItem))
                {
                    openBracketStack.Push(charItem);
                }
                
                if (!openBracketStack.Any() || !CloseBrackets.Contains(charItem))
                {
                    continue;
                }

                if (AreBracketsMatching(openBracketStack.Peek(), charItem))
                {
                    openBracketStack.Pop();
                }
                else
                {
                    ErrorMessage = "Wrong closing order";
                    return false;
                }
            }

            if (openBracketStack.Any())
            {
                ErrorMessage = $"Missing closing '{BracketPairs[openBracketStack.Peek()]}'";
                return false;
            }
            return true;
        }
        
        private static bool AreBracketsMatching(char openBracket, char closeBracket)
        {
            return BracketPairs[openBracket] == closeBracket;
        }
    }
}