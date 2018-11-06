using System;
using System.Collections.Generic;
using System.Linq;
using Wikipedia.AppContracts.Strategies;
using Wikipedia.Helpers;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.Core.Strategies
{
    public class InverseDocFrequencyTextRanker : IAnswerRanker
    {
        readonly string[] _commonWords = CommonWordsStore.GetFrequentWords();

        /// <summary>
        /// Ranks candidate texts against textToMatch based on:
        /// 1. How many words match (excluding common words)
        /// 2. Their relative significance (inverse document frequency)
        /// 3. Calculates weighted rank by dividing by candiate length
        /// </summary>
        /// <param name="textToMatch"></param>
        /// <param name="candidates"></param>
        /// <param name="docWordFrequency"></param>
        /// <returns></returns>
        public IEnumerable<AnswerRank> GetWordMatchRanks(string textToMatch, 
            IEnumerable<string> candidates, IEnumerable<WordFrequency> docWordFrequency)
        {
            var ranks = new List<AnswerRank>();

            foreach (var target in candidates)
            {
                var scores = GetAnswerScore(textToMatch, target, docWordFrequency);
                ranks.Add(new AnswerRank
                {
                    Answer = target,
                    WordMatchScore = scores.WordMatchScore,
                    WeightedScore = scores.WeightedScore
                });
            }

            return ranks;
        }

        private (int WordMatchScore, decimal WeightedScore) GetAnswerScore(string lineToMatch, string answer, IEnumerable<WordFrequency> docWordFrequency)
        {
            var wordMatches = StringProcessor.GetUniqueWordMatch(lineToMatch, answer, _commonWords);

            var standardUpperLimit = docWordFrequency.Max(w => w.Frequency);
            var wordPriorityScore = 0;
            foreach (var word in wordMatches)
            {                
                var relativeWordPriority = standardUpperLimit - (docWordFrequency.FirstOrDefault(iw => iw.Word == word)?.Frequency ?? 0);
                wordPriorityScore += relativeWordPriority;
            }           

            var weightedScore = (wordPriorityScore / (decimal)answer.Length) * 10000;
            return (wordPriorityScore, Math.Round(weightedScore, 2));
        }

        private (int WordMatch, decimal WeightedScore) GetAnswerScore0(string lineToMatch, string answer)
        {
            var wordMatchCount = StringProcessor.GetUniqueWordMatchCount(lineToMatch, answer, _commonWords);
            var weightedScore = (wordMatchCount / (decimal)answer.Length) * 10000;
            return (wordMatchCount, Math.Round(weightedScore, 2));
        }
    }
}
