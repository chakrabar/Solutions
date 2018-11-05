using System.Collections.Generic;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts
{
    public interface ILineMatchRanker
    {
        IEnumerable<LineRank> GetRelevantLinesWithRank(IEnumerable<WordPriority> keywords, IEnumerable<LineIndex> lines);
    }
}
