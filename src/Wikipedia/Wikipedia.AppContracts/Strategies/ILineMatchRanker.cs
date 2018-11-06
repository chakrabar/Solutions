using System.Collections.Generic;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts.Strategies
{
    public interface ILineMatchRanker
    {
        IEnumerable<LineRank> GetRelevantLinesWithRank(IEnumerable<WordPriority> targetKeywordPriorities,
            IEnumerable<LineIndex> candidateLinesWithWordIndex);
    }
}
