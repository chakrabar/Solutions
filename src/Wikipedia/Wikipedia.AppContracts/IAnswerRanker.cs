using System.Collections.Generic;
using Wikipedia.Models;

namespace Wikipedia.AppContracts
{
    public interface IAnswerRanker
    {
        IEnumerable<AnswerRank> GetWordMatchRanks(string lineToMatch, IEnumerable<string> targets);
    }
}
