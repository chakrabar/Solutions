using System.Collections.Generic;
using Wikipedia.Models;

namespace Wikipedia.AppContracts
{
    public interface IAnswerRanker
    {
        IEnumerable<LineRank> GetWordMatchRanks(string lineToMatch, IEnumerable<string> targets);
    }
}
