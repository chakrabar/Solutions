using System.Collections.Generic;
using System.Linq;
using Wikipedia.AppContracts.Strategies;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.Core.Strategies
{
    public class WordPriorityLineRanker : IQuestionToILineMatcher
    {
        /// <summary>
        /// Scores and returns relevant lines for a question (with word priority)
        /// 1. For each word in question, find all lines with the current word
        /// 2. For each matched lines, score as: word freq in line * word priority
        /// 3. When scoring done for all words in question, aggregate scores for each line to get final score
        /// </summary>
        /// <param name="questionKeywordPriorities"></param>
        /// <param name="candidateLinesWithWordIndex"></param>
        /// <returns></returns>
        public IEnumerable<LineRank> GetRelevantLinesWithRank(IEnumerable<WordPriority> questionKeywordPriorities,
            IEnumerable<LineIndex> candidateLinesWithWordIndex)
        {
            var allLinesWithInitialRank = new List<LineRank>();

            foreach (var term in questionKeywordPriorities)
            {
                var allLinesWithTerm = candidateLinesWithWordIndex
                    .Where(l => l.WordIndex.Any(w => w.Word == term.Word));
                foreach (var line in allLinesWithTerm)
                {
                    allLinesWithInitialRank.Add(new LineRank
                    {
                        LineId = line.Id,
                        Line = line.Line,
                        Score = line.WordIndex.First(wi => wi.Word == term.Word).Frequency * term.Priority
                    });
                }
            }

            var linesWithAgreegatedRank = allLinesWithInitialRank
                .GroupBy(l => l.LineId)
                .Select(g => new LineRank
                {
                    LineId = g.Key,
                    Line = g.First().Line,
                    Score = g.Sum(sameLine => sameLine.Score)
                });

            var result = linesWithAgreegatedRank.Where(lr => lr.Score > 0);

            return result;
        }
    }
}
