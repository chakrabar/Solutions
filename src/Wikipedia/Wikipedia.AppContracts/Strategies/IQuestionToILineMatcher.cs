using System.Collections.Generic;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts.Strategies
{
    public interface IQuestionToILineMatcher
    {
        IEnumerable<LineRank> GetRelevantLinesWithRank(IEnumerable<WordPriority> questionKeywordPriorities,
            IEnumerable<LineIndex> candidateLinesWithWordIndex);
    }
}
