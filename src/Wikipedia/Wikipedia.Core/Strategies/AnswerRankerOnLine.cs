using System.Collections.Generic;
using Wikipedia.AppContracts;
using Wikipedia.Core.TextHelpers;
using Wikipedia.Models;

namespace Wikipedia.Core.Strategies
{
    public class AnswerRankerOnLine : IAnswerRanker
    {
        public IEnumerable<LineRank> GetWordMatchRanks(string lineToMatch, IEnumerable<string> targets)
        {
            var ranks = new List<LineRank>();

            foreach (var target in targets)
            {
                ranks.Add(new LineRank
                {
                    Line = target,
                    Rank = StringProcessor.GetWordMatchCount(lineToMatch, target)
                });
            }

            return ranks;
        }
    }
}
