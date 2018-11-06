using System.Collections.Generic;
using System.Linq;
using Wikipedia.AppContracts.Strategies;
using Wikipedia.Helpers;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.Core.Strategies
{
    public class InverseDocFrequencyWordPrioritizer : IKeywordPrioritizer
    {
        readonly string[] _questionWords = CommonWordsStore.GetQuestionWords();
        readonly string[] _frequentWords = CommonWordsStore.GetFrequentWords();

        public IEnumerable<WordPriority> GetWordsWithPriority(string sentence,
            IEnumerable<WordFrequency> docWordFrequency)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                return null;

            var words = StringProcessor.GetWordsLower(sentence);
            var nonQuestionWords = StringProcessor.RemoveWords(words, _questionWords);
            var meaningfulWords = StringProcessor.RemoveWords(nonQuestionWords, _frequentWords);

            //arrange by descending order of frequency in original content (take 0 if not found)
            var standardUpperLimit = docWordFrequency.Max(w => w.Frequency);
            var nonQuestionWordsByValue = meaningfulWords
                .Select(w => new WordPriority
                {
                    Word = w,
                    Priority = standardUpperLimit - (docWordFrequency.FirstOrDefault(iw => iw.Word == w)?.Frequency ?? 0)
                })
                .OrderBy(wp => wp.Priority);
                //.Select((wp, idx) => new WordPriority { Word = wp.Word, Priority = idx + 1 });

            return nonQuestionWordsByValue;
        }
    }
}
