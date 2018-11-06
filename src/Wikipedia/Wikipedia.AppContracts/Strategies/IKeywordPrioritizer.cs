using System.Collections.Generic;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts.Strategies
{
    public interface IKeywordPrioritizer
    {
        IEnumerable<WordPriority> GetWordsWithPriority(ContentIndex indexData, string sentence);
    }
}
