using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Wikipedia.Core.TextHelpers
{
    public static class StringProcessor
    {
        public static IEnumerable<string> GetWordsLower(string sentence)
        {
            sentence = sentence.ToLower();
            return Regex.Split(sentence, @"\b[\w']*\b"); //TODO: check
        }

        public static IEnumerable<string> RemoveWords(IEnumerable<string> target, IEnumerable<string> toRemove)
        {
            var words = target.Distinct();
            return words.Except(toRemove);
        }

        public static int GetWordMatchCount(string left, string right)
        {
            var leftWords = GetWordsLower(left);
            var rightWords = GetWordsLower(right);

            return leftWords.Intersect(rightWords).Count();
        }
    }
}
