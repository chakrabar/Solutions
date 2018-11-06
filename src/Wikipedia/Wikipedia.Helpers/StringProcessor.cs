using System.Collections.Generic;
using System.Linq;

namespace Wikipedia.Helpers
{
    public static class StringProcessor
    {
        public static IEnumerable<string> GetWordsLower(string sentence)
        {
            sentence = sentence.ToLower();
            return sentence.Split(new[] 
                { ' ', '.', ',', ':', ';', '-', '~', '`', '!', '@', '#', '&', '_', '+', '/', '?', '(', ')', '{', '}', '[', ']' })
                .Where(part => !string.IsNullOrWhiteSpace(part));
        }

        public static IEnumerable<string> GetSentences(string paragraph)
        {
            return paragraph.Split(new[]
                { '.', '?', '!' })
                .Select(s => s.Trim())
                .Where(part => !string.IsNullOrWhiteSpace(part));
        }

        public static IEnumerable<string> GetAllParts(string paragraph, params char[] separators)
        {
            return paragraph.Split(separators)
                .Select(s => s.Trim())
                .Where(part => !string.IsNullOrWhiteSpace(part));
        }

        public static IEnumerable<string> RemoveWords(IEnumerable<string> target, IEnumerable<string> toRemove)
        {
            var words = target.Distinct();
            return words.Except(toRemove);
        }

        public static int GetUniqueWordMatchCount(string left, string right, string[] wordsToIgnore)
        {
            var leftWords = GetQniqueWordsLower(left, wordsToIgnore);
            var rightWords = GetQniqueWordsLower(right, wordsToIgnore);

            return leftWords.Intersect(rightWords).Count();
        }

        public static IEnumerable<string> GetQniqueWordsLower(string sentence, params string[] wordsToRemove)
        {
            var words = GetWordsLower(sentence);
            if (wordsToRemove != null)
                words = RemoveWords(words, wordsToRemove);
            return words.Distinct();
        }
    }
}
