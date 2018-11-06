using System.Collections.Generic;
using System.Linq;
using Wikipedia.AppContracts;
using Wikipedia.Helpers;
using Wikipedia.Models.Index;

namespace Wikipedia.Core
{
    public class ContentIndexBuilder : IContentIndexBuilder
    {
        public ContentIndex Build(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
                return null;

            var result = new ContentIndex
            {
                Content = paragraph,
                Lines = new List<LineIndex>(),
                AllWords = new List<WordFrequency>()
            };

            var lines = StringProcessor.GetSentences(paragraph);

            var lineIndex = 0;
            foreach (var line in lines)
            {
                var words = StringProcessor.GetWordsLower(line);
                result.Lines.Add(new LineIndex
                {
                    Id = ++lineIndex,
                    Line = line,
                    WordIndex = GetWordFrequency(words).ToList()
                });
            }

            var allWordCombinationsInAllLines = result.Lines.Select(l => l.WordIndex);
            result.AllWords = GetCombinedWordFrequency(allWordCombinationsInAllLines)
                .ToList();

            return result;
        }

        private IEnumerable<WordFrequency> GetWordFrequency(IEnumerable<string> words)
        {
            var wordFreq = words
                .Select(wrd => wrd.ToLower())
                .GroupBy(w => w)
                .Select(grp => new WordFrequency
                {
                    Word = grp.Key,
                    Frequency = grp.Count()
                });
            return wordFreq;
        }

        private IEnumerable<WordFrequency> GetCombinedWordFrequency(IEnumerable<IEnumerable<WordFrequency>> allWordCombinations)
        {
            var allWords = allWordCombinations
                .SelectMany(comb => comb);

            return allWords
                .GroupBy(w => w.Word)
                .Select(g => new WordFrequency
                {
                    Word = g.Key,
                    Frequency = g.Sum(wf => wf.Frequency)
                });
        }
    }
}
