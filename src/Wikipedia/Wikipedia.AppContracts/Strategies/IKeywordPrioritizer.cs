using System.Collections.Generic;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts.Strategies
{
    public interface IKeywordPrioritizer
    {
        IEnumerable<WordPriority> GetWordsWithPriority(string sentence,
            IEnumerable<WordFrequency> docWordFrequency);
    }
}
