using System;
using System.Collections.Generic;
using Wikipedia.AppContracts;
using Wikipedia.Helpers;
using Wikipedia.Models;

namespace Wikipedia.Core.Strategies
{
    public class AnswerRankerOnLine : IAnswerRanker
    {
        readonly string[] _commonWords = CommonWordsFactory.GetFrequentWords();

        public IEnumerable<AnswerRank> GetWordMatchRanks(string lineToMatch, IEnumerable<string> targets)
        {
            var ranks = new List<AnswerRank>();

            foreach (var target in targets)
            {
                var scores = GetAnswerScore(lineToMatch, target);
                ranks.Add(new AnswerRank
                {
                    Answer = target,
                    WordMatch = scores.WordMatch,
                    WeightedScore = scores.WeightedScore
                });
            }

            return ranks;
        }

        private (int WordMatch, decimal WeightedScore) GetAnswerScore(string lineToMatch, string answer)
        {
            var wordMatchCount = StringProcessor.GetUniqueWordMatchCount(lineToMatch, answer, _commonWords);
            var weightedScore = (wordMatchCount / (decimal)answer.Length) * 10000;
            return (wordMatchCount, Math.Round(weightedScore, 2));
        }
    }
}
