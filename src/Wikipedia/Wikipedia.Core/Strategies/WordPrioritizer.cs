using System.Collections.Generic;
using System.Linq;
using Wikipedia.AppContracts;
using Wikipedia.Helpers;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.Core.Strategies
{
    public class WordPrioritizer : IKeywordPrioritizer
    {
        readonly string[] _questionWords = CommonWordsFactory.GetQuestionWords();
        readonly string[] _frequentWords = CommonWordsFactory.GetFrequentWords();

        public IEnumerable<WordPriority> GetWordsWithPriority(ContentIndex indexData, string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                return null;

            var words = StringProcessor.GetWordsLower(sentence);
            var nonQuestionWords = StringProcessor.RemoveWords(words, _questionWords);
            var meaningfulWords = StringProcessor.RemoveWords(nonQuestionWords, _frequentWords);

            //arrange by descending order of frequency in original content (take 0 if not found)
            var standardUpperLimit = indexData.AllWords.Max(w => w.Frequency);
            var nonQuestionWordsByValue = meaningfulWords
                .Select(w => new WordPriority
                {
                    Word = w,
                    Priority = standardUpperLimit - (indexData.AllWords.FirstOrDefault(iw => iw.Word == w)?.Frequency ?? 0)
                })
                .OrderBy(wp => wp.Priority);

            return nonQuestionWordsByValue;
        }
    }
}
