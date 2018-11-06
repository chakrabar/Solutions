using System.Collections.Generic;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts.Strategies
{
    public interface IAnswerRanker
    {
        IEnumerable<AnswerRank> GetWordMatchRanks(string lineToMatch, 
            IEnumerable<string> targets, IEnumerable<WordFrequency> docWordFrequency);
    }
}
