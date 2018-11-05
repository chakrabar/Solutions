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
        readonly string[] questionWords = QuestionWordFactory.GetCommonQuestionWords();

        public IEnumerable<WordPriority> ArrangeByPriority(ContentIndex indexData, string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                return null;

            var words = StringProcessor.GetWordsLower(sentence);
            var nonQuestionWords = StringProcessor.RemoveWords(words, questionWords);

            //arrange by descending order of frequency in original content (take 0 if not found)
            var standardUpperLimit = indexData.AllWords.Max(w => w.Frequency);
            var nonQuestionWordsByValue = nonQuestionWords                
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
